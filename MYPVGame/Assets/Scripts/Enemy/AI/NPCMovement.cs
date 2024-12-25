using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _approachThreshhold = 3f;
    [SerializeField] private float _rotationSpeed = 3f;
    [SerializeField] private float _pushForce = 10f;
    [SerializeField] private float _pushRadius = 1f;
    private EnemyRadar _enemyRadar;

    private Vector2 _movementVector = Vector2.zero;
    private Rigidbody2D _rigidbody;
    private Quaternion _targetRotation;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _enemyRadar = GetComponentInChildren<EnemyRadar>();
    }

    private void FixedUpdate()
    {
        transform.rotation = SmoothRotation(transform.rotation, _targetRotation);
        PushAwayFromNearbyEnemies();
        _rigidbody.velocity = _movementVector.normalized * _moveSpeed;
    }

    public void ApproachPosition(Vector3 targetPosition)
    {
        var directionToTarget = (targetPosition - transform.position).normalized;
        var distanceToTarget = Vector2.Distance(transform.position, targetPosition);
        //Debug.Log("Dir: " + directionToTarget);
        //Debug.Log("dist: " + distanceToTarget);
        
        _movementVector = (distanceToTarget - _approachThreshhold) * directionToTarget;
        if (_movementVector.sqrMagnitude < 0.01f) 
            _movementVector = Vector2.zero;
        //Debug.Log(_movementVector);
        // Update rotation
        _targetRotation = Quaternion.Euler(0, 0, GetChaseAngle(targetPosition));
    }

    public void FleeFromPosition(Vector3 targetPosition)
    {
        _movementVector = -(targetPosition - transform.position).normalized;
        _targetRotation = Quaternion.Euler(0, 0, GetChaseAngle(targetPosition));
    }

    public void ApplyKnockBack(Collider2D collider) //TODO: Bind with DealDamageOnCollision, work on calling in FixedUpdate
    {
        Vector2 knockbackVector = (transform.position - collider.attachedRigidbody.transform.position).normalized;
        _rigidbody.AddForce(knockbackVector, ForceMode2D.Impulse);
    }

    public void StopMovement()
    {
        _movementVector = Vector2.zero;
    }

    public float GetMovementSpeed()
    {
        return _moveSpeed;
    }
    
    private float GetChaseAngle(Vector3 targetPosition)
    {
        return Vector3.SignedAngle(Vector3.up, (targetPosition - transform.position).normalized, Vector3.forward);
    }

    private Quaternion SmoothRotation(Quaternion current, Quaternion target)
    {
        var angle = Quaternion.Angle(current, target);
        if (angle < 6) // 180 / 30 = 6
            return target;
        var t = Mathf.Clamp01(1 - angle / 180);
        var easedT = QuadraticEaseInOut(t) * _rotationSpeed * Time.fixedDeltaTime;
        return Quaternion.Slerp(current, target, easedT);
    }

    /*
     * Quadratic easing function that ensures the area under the F(x) curve is 1:
     * 4x^2 when x < 0.5
     * -4x^2 + 8x - 2 when x >= 0.5
     * In code these are first derivative (f(x)) of the functions cuz we are using it for interpolation
     */
    private static float QuadraticEaseInOut(float x)
    {
        return x < 0.5f ? 8 * x : -8 * x + 8;
    }

    private void PushAwayFromNearbyEnemies()
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, _pushRadius, _enemyRadar.GetEnemyLayerMask()); // Assuming you have a method to get the enemy layer mask in EnemyRadar

        foreach (Collider2D enemyCollider in nearbyEnemies)
        {
            if (enemyCollider != GetComponent<Collider2D>()) // Don't push self
            {
                Vector2 pushDirection = (transform.position - enemyCollider.transform.position).normalized;
                _rigidbody.AddForce(pushDirection * _pushForce);
            }
        }
    }
}
