using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFix : MonoBehaviour
{
    Quaternion _startRotation;
    void Awake()
    {
        _startRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        transform.rotation = _startRotation;
    }
}
