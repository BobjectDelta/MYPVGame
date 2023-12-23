using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingInput : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Camera _camera;

    [SerializeField] private Shooting _shootingType;

    Vector2 mousePosition;

    private void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
            _shootingType.Shoot();
    }

    private void FixedUpdate()
    {
        Vector2 lookDirection = mousePosition - _rigidbody2D.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        _rigidbody2D.rotation = angle;
    }

    public Shooting GetShootingType() 
    { 
        return _shootingType; 
    }
}
