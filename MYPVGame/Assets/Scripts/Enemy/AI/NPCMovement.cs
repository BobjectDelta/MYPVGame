using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1;
    [SerializeField] private float _approachThreshhold = 3;
    [SerializeField] private float _rotationSpeed = 3f;
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
        // Debug.Log(_movementVector.x + " " + _movementVector.y);
        transform.rotation = SmoothRotation(transform.rotation, _targetRotation);
        _rigidbody.velocity = _movementVector * _moveSpeed;
    }

    public void ApproachPosition(Vector3 targetPosition)
    {
        _movementVector = (targetPosition - transform.position).normalized;
        _targetRotation = Quaternion.Euler(0, 0, GetChaseAngle(targetPosition));

        var distanceToTarget = Vector2.Distance(transform.position, targetPosition);
        _movementVector = (targetPosition - transform.position).normalized;
        var deltaDistance = distanceToTarget - _approachThreshhold;

        if (deltaDistance < 0.8f)
            _movementVector *= -1;
        else if (deltaDistance < 1f)
            StopMovement();
    }

    public void FleeFromPosition(Vector3 targetPosition)
    {
        _movementVector = -(targetPosition - transform.position).normalized;
        _targetRotation = Quaternion.Euler(0, 0, GetChaseAngle(targetPosition));
    }

    public void StopMovement()
    {
        _movementVector = Vector2.zero;
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
     * In code this are first derivative (f(x)) of the functions cuz we are using it for interpolation
     */
    private static float QuadraticEaseInOut(float x)
    {
        return x < 0.5f ? 8 * x : -8 * x + 8;
    }
}