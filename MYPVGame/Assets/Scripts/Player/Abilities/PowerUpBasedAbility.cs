using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBasedAbility : AbstractAbility
{
    [SerializeField] private float _effectTime;
    [SerializeField] private GameObject _powerUp;
    [SerializeField] private GameObject _playerEffect;
    
    public override void ActivateAbility()
    {
        if (_canActivate)
        {
            _powerUp.GetComponent<PowerUp>().SetEffectTime(_effectTime);
            Instantiate(_powerUp, transform.position, Quaternion.identity);
            if (_playerEffect != null)
                Instantiate(_playerEffect, transform.localPosition, Quaternion.identity, transform);
            StartCoroutine(SetActivationAbility());
        }
    }
}
