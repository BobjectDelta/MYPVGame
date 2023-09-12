using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 4f;

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Camera _camera;

    Vector2 movementVector;
    Vector2 mousePosition;
    void Update()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");

        mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        //_rigidbody2D.AddForce(movementVector * _movementSpeed * Time.fixedDeltaTime);
        _rigidbody2D.velocity = movementVector * _movementSpeed;

        Vector2 lookDirection = mousePosition - _rigidbody2D.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        _rigidbody2D.rotation = angle;
    }
}
