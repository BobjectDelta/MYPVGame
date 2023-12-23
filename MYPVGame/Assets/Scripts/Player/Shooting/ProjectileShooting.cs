using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileShooting : Shooting
{
    [SerializeField] private float _shootingForce = 20f;
    [SerializeField] private float _bulletLifeTime = 1;
    private int _additionalBullets = 0;

    public override void Shoot()
    {
        if (!_canShoot)
            return;
        for (int i = 0; i < _additionalBullets + 1; i++)
        {
            foreach (var bulletSpawnPoint in _bulletSpawnPoints)
            {
                GameObject bullet = Instantiate(_bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                Rigidbody2D bulletRigidBody2D = bullet.GetComponent<Rigidbody2D>();

                Vector3 bulletSpread = bullet.transform.rotation.eulerAngles;
                bulletSpread.z += Random.Range(-_spreadDegrees, _spreadDegrees);
                bullet.transform.rotation = Quaternion.Euler(bulletSpread);

                bullet.GetComponent<PlayerBullet>().SetDamage(_damage);
                bullet.GetComponent<TimedObjectDestruction>().SetLifeTime(_bulletLifeTime);

                bulletRigidBody2D.AddForce(bullet.transform.up * _shootingForce, ForceMode2D.Impulse);
            }
        }
        StartCoroutine(SetShootAbility());
    } 

    public override void ChangeBulletDamage(float damagePoints)
    {
        _damage += damagePoints;
        if (_damage < 1)
            _damage = 1f;
    }

    public override void ChangeFireRate(float fireRatePoints)
    {
        _fireRateDelay += fireRatePoints;
        if (_fireRateDelay <= 0)
            _fireRateDelay = 0.05f;
    }

    public override void ChangeShootingSpread(float spreadDegreesPoints)
    {
        _spreadDegrees += spreadDegreesPoints;
        if (_spreadDegrees < 0)
            _spreadDegrees = 0;
    }

    public override void ChangeShootingForce(float shootingForcePoints)
    {
        _bulletLifeTime *= _shootingForce / (_shootingForce + shootingForcePoints);
        _shootingForce += shootingForcePoints;
        if (_shootingForce <= 0)
            _shootingForce = 1;
    }

    public override void ChangeAdditionalBulletAmount(int  amount)
    {
        _additionalBullets += amount;
        if (_additionalBullets < 0)
            _additionalBullets = 0;
    }
}
