using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 4f;

    [SerializeField] private Rigidbody2D _rigidbody2D;

    Vector2 movementVector;
    void Update()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = movementVector * _movementSpeed;
    }

    public void ChangeMovementSpeed(float speedPoints)
    {
        _movementSpeed += speedPoints;
        if (_movementSpeed < 1)
            _movementSpeed = 1;
    }

    public float GetMovementSpeed()
    {
        return _movementSpeed;
    }

}
