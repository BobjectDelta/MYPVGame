using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChaseBehaviour : AIBehaviour
{
    [SerializeField] private float _moveSpeed = 1;

    private Vector2 _movementVector = Vector2.zero;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    public override void PerformAction(AIDetector detector)
    {
        if (detector.isTargetVisible)
        {
            _movementVector = GetChaseMovementVector(detector);
            transform.rotation = Quaternion.Euler(0, 0, GetChaseAngle(detector));
        }
        else
            _movementVector = Vector2.zero;
    }

    private Vector2 GetChaseMovementVector(AIDetector detector)
    {
        Vector2 movementVector = (detector.Target.position - transform.position).normalized;
        return movementVector;
    }

    private float GetChaseAngle(AIDetector detector) 
    {
        float rotationAngle = Vector3.SignedAngle(Vector3.up, (detector.Target.position - transform.position).normalized, Vector3.forward);
        return rotationAngle;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _movementVector * _moveSpeed;
    }
}
