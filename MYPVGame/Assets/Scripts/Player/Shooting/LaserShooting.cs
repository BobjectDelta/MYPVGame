using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooting : Shooting
{
    [SerializeField] private LineRenderer _shotEffect;
    [SerializeField] private float _maxShotDistance = 1;
    [SerializeField] private float _shotEffectTime = 0.5f;

    private Vector3 _shootingDirection;
    private Vector3 _bulletSpawnPointPosition;

    private void Awake()
    {
        _shotEffect.enabled = false;
    }

    private void Update()
    {
        //_shootingDirection = Quaternion.AngleAxis(Random.Range(-_spreadDegrees, _spreadDegrees), Vector3.forward) * _bulletSpawnPoints[0].up;
    }
    public override void Shoot() 
    {
        if (!_canShoot)
            return;
        foreach (var bulletSpawnPoint in _bulletSpawnPoints)
        {
            _shootingDirection = Quaternion.AngleAxis(Random.Range(-_spreadDegrees, _spreadDegrees), Vector3.forward) * bulletSpawnPoint.up;
            _bulletSpawnPointPosition = bulletSpawnPoint.position;
            RaycastHit2D raycastHitInfo = Physics2D.Raycast(_bulletSpawnPointPosition, _shootingDirection, _maxShotDistance);
            _shotEffect.SetPosition(0, _bulletSpawnPointPosition);

            if (raycastHitInfo)
            {
                Health health = raycastHitInfo.transform.GetComponent<Health>();
                if (!raycastHitInfo.transform.CompareTag("Player") && health != null)
                {
                    health.TakeDamage(_damage);
                }

                //Instantiate(_impactEffect, raycastHitInfo.point, Quaternion.identity);
                _shotEffect.SetPosition(1, raycastHitInfo.point);
            }
            else
                _shotEffect.SetPosition(1, _bulletSpawnPointPosition + _shootingDirection * _maxShotDistance);

            StartCoroutine(DisableShotEffect());
        }
        StartCoroutine(SetShootAbility());
    }

    public IEnumerator DisableShotEffect()
    {
        _shotEffect.enabled = true;
        yield return new WaitForSeconds(_shotEffectTime);
        _shotEffect.enabled = false;
    }
}
