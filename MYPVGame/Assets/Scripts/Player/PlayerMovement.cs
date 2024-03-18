using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 4f;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private Vector2 _movementVector;

    public void OnMove(InputAction.CallbackContext context)
    {
        _movementVector = context.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        _rigidbody2D.velocity = _movementVector * _movementSpeed;
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
