using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

public class ShootingInput : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Camera _camera;
    [SerializeField] private Shooting _shootingType;

    //Vector2 mousePosition;
    private Vector2 _lastLookDirection;
    private Vector2 _lookDirection;
    private float _angle;


    private void Start()
    {
        _camera = Camera.main;
        _camera.GetComponent<FollowingCamera>().SetTarget(gameObject);
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        _lookDirection = context.ReadValue<Vector2>();
    }

    void Update()
    {
        //mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        //if (Input.GetMouseButton(0))

        if (Mathf.Abs(_lookDirection.x) >= 0.1f || Mathf.Abs(_lookDirection.y) >= 0.1f)
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
        if (_lookDirection != Vector2.zero)        
            //lookDirection = _shootingJoystick.Direction;//mousePosition - _rigidbody2D.position;
            _lastLookDirection = _lookDirection;
        
        _angle = Mathf.Atan2(_lastLookDirection.y, _lastLookDirection.x) * Mathf.Rad2Deg - 90f;
        _rigidbody2D.rotation = _angle;
    }

    public Shooting GetShootingType() 
    { 
        return _shootingType; 
    }
}
