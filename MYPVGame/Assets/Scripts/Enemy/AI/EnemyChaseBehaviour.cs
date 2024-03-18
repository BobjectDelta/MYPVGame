using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseBehaviour : EnemyBehaviour
{
    [SerializeField] private float _moveSpeed = 1;

    private Vector2 _movementVector = Vector2.zero;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public override void ExecuteAction(EnemyRadar enemyRadar)
    {
        if (enemyRadar.isTargetVisible)
        {
            _movementVector = GetChaseMovementVector(enemyRadar);
            transform.rotation = Quaternion.Euler(0, 0, GetChaseAngle(enemyRadar));
        }
        else
            _movementVector = Vector2.zero;
    }

    private Vector2 GetChaseMovementVector(EnemyRadar enemyRadar)
    {
        Vector2 movementVector = (enemyRadar.GetRadarTarget().position - transform.position).normalized;
        return movementVector;
    }

    private float GetChaseAngle(EnemyRadar enemyRadar)
    {
        float rotationAngle = Vector3.SignedAngle(Vector3.up, (enemyRadar.GetRadarTarget().position - transform.position).normalized, Vector3.forward);
        return rotationAngle;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _movementVector * _moveSpeed;
    }
}
