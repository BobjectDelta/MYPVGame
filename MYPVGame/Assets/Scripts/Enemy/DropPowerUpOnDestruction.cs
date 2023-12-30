using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPowerUpOnDestruction : MonoBehaviour
{
    [SerializeField][Range(0, 1)] private float _dropChance = 0.5f;
    [SerializeField] private List<PowerUp> _powerUps = new List<PowerUp>();
    public void DropPowerUp()
    {
        if (_dropChance >= Random.Range(0, 1f))
            Instantiate(_powerUps[Random.Range(0, _powerUps.Count)], transform.position, Quaternion.identity);
    }
}
