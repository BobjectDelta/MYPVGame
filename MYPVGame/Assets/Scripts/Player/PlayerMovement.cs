using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 4f;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    //[SerializeField] private Joystick _movementJoystick;
    //[SerializeField] private OnScreenStick _movementJoystick;


    Vector2 movementVector;

    void Update()
    {
        //movementVector.x = Input.GetAxisRaw("Horizontal");
        //movementVector.y = Input.GetAxisRaw("Vertical");
        //movementVector.x = _movementJoystick.Horizontal;
        //movementVector.y = _movementJoystick.Vertical;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementVector = context.ReadValue<Vector2>();
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

    //public void SetMovementJoystick(GameObject joystick)
    //{
    //    //_movementJoystick = joystick.GetComponent<Joystick>();
    //    _movementJoystick = joystick.GetComponent<OnScreenStick>();
    //}

    public float GetMovementSpeed()
    {
        return _movementSpeed;
    }

}
