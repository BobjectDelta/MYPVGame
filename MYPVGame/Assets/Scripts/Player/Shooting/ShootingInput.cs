using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

public class ShootingInput : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Camera _camera;
    //[SerializeField] private OnScreenStick _shootingJoystick;
    [SerializeField] private Shooting _shootingType;

    Vector2 mousePosition;
    Vector2 lastLookDirection;
    Vector2 lookDirection;
    float angle;


    private void Start()
    {
        _camera = Camera.main;
        _camera.GetComponent<FollowingCamera>().SetTarget(gameObject);
        //_shootingJoystick = GameObject.Find("ShootingJoystick").GetComponent<Joystick>();
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        lookDirection = context.ReadValue<Vector2>();
    }

    void Update()
    {
        //mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        //if (Input.GetMouseButton(0))

        //if (Mathf.Abs(_shootingJoystick.Horizontal) >= 0.1f || Mathf.Abs(_shootingJoystick.Vertical) >= 0.1f)
            //_shootingType.Shoot();
        if (Mathf.Abs(lookDirection.x) >= 0.1f || Mathf.Abs(lookDirection.y) >= 0.1f)
            _shootingType.Shoot();
    }

    private void FixedUpdate()
    {
        //if (_shootingJoystick.Direction != Vector2.zero)
        //{
        //    lookDirection = _shootingJoystick.Direction;//mousePosition - _rigidbody2D.position;
        //    lastLookDirection = lookDirection;
        //}
        //angle = Mathf.Atan2(lastLookDirection.y, lastLookDirection.x) * Mathf.Rad2Deg - 90f;
        //_rigidbody2D.rotation = angle;
        if (lookDirection != Vector2.zero)
        {
            //lookDirection = _shootingJoystick.Direction;//mousePosition - _rigidbody2D.position;
            lastLookDirection = lookDirection;
        }
        angle = Mathf.Atan2(lastLookDirection.y, lastLookDirection.x) * Mathf.Rad2Deg - 90f;
        _rigidbody2D.rotation = angle;
    }

    //public void SetShootingJoystick(GameObject joystick)
    //{
    //    //_shootingJoystick = joystick.GetComponent<Joystick>();
    //    _shootingJoystick = joystick.GetComponent<OnScreenStick>();
    //}

    public Shooting GetShootingType() 
    { 
        return _shootingType; 
    }
}