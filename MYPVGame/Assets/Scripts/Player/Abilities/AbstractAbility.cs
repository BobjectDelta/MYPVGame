using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAbility : MonoBehaviour
{
    [SerializeField] private float _rechargeTime = 10;

    protected bool _canActivate = true;

    public abstract void ActivateAbility();

    public IEnumerator SetActivationAbility()
    {
        _canActivate = false;
        yield return new WaitForSeconds(_rechargeTime);
        _canActivate = true;
    }
}
