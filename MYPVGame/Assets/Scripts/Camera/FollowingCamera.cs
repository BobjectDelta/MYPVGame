using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private float _smoothingSpeed;
    [SerializeField] private GameObject _target;
    private Vector3 _velocity = Vector3.zero;

    private Vector3 _targetPosition;

    private void Start()
    {
        _target = GameObject.FindWithTag("Player");
        if (_target)
            transform.position = _target.transform.position;
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            _targetPosition = _target.transform.position;
            _targetPosition.z = -10;
            transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _velocity, _smoothingSpeed);
        }
    }
}
