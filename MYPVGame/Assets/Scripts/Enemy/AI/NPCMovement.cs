using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 1;

    private Vector2 _movementVector = Vector2.zero;
    private Rigidbody2D _rigidbody;
    private EnemyRadar _enemyRadar;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _enemyRadar = GetComponentInChildren<EnemyRadar>();
    }

    public void ApproachPosition(Vector3 targetPosition, int approachThreshhold = 0)
    {
        _movementVector = (targetPosition - transform.position).normalized;
        transform.rotation = Quaternion.Euler(0, 0, GetChaseAngle(targetPosition));
    }

    public void StopMovement()
    {
        _movementVector = Vector2.zero;
    }

    private float GetChaseAngle(Vector3 targetPosition )
    {
        float rotationAngle = Vector3.SignedAngle(Vector3.up, (targetPosition - transform.position).normalized, Vector3.forward);
        return rotationAngle;
    }

    private void FixedUpdate()
    {
        Debug.Log(_movementVector.x + " " + _movementVector.y);
        _rigidbody.velocity = _movementVector * _moveSpeed;
    }
}
