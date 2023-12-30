using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBasedAbility : AbstractAbility
{
    [SerializeField] private float _effectTime;
    [SerializeField] private GameObject _powerUp;
    
    public override void ActivateAbility()
    {
        _powerUp.GetComponent<PowerUp>().SetEffectTime(_effectTime);
        Instantiate(_powerUp, transform.position, Quaternion.identity);
    }
}
