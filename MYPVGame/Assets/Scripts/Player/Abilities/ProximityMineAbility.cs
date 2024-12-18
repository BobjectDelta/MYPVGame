using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityMineAbility : AbstractAbility
{
    [SerializeField] private GameObject _minePrefab;

    public override void ActivateAbility()
    {
        if (_canActivate)
        {
            Instantiate(_minePrefab, transform.position, Quaternion.identity);
            StartCoroutine(SetActivationAbility());
        }
    }
}
