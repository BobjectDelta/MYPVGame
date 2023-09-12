using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float _smoothingSpeed;
    [SerializeField] private GameObject _target;

    private Vector3 _targetPosition;
    void Update()
    {
        if (_target)
        {
            _targetPosition = _target.transform.position;
            _targetPosition.z = -10;
            this.transform.position = Vector3.Lerp(this.transform.position, _targetPosition, _smoothingSpeed * Time.deltaTime);
        }
    }
}
