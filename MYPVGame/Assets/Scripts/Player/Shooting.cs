using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private float _shootingForce = 20f;
    [SerializeField] private float _spreadDegrees;
    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _bulletLifeTime = 1;
    [SerializeField] private float _fireRateDelay = 0.5f;
    private int _additionalBullets = 0;
    private float _remainingDelay = 0;

    void Update()
    {
        if (_remainingDelay <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
                Shoot();
        }
        else       
            _remainingDelay -= Time.deltaTime;
        
    }

    private void Shoot()
    {
        for (int i = 0; i < _additionalBullets+1; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
            Rigidbody2D bulletRigidBody2D = bullet.GetComponent<Rigidbody2D>();

            Vector3 bulletSpread = bullet.transform.rotation.eulerAngles;
            bulletSpread.z += Random.Range(-_spreadDegrees, _spreadDegrees);
            bullet.transform.rotation = Quaternion.Euler(bulletSpread);

            bullet.GetComponent<PlayerBullet>().SetDamage(_damage);
            bullet.GetComponent<TimedObjectDestruction>().SetLifeTime(_bulletLifeTime);

            bulletRigidBody2D.AddForce(bullet.transform.up * _shootingForce, ForceMode2D.Impulse);
        }

        _remainingDelay = _fireRateDelay;
    }

    public void ChangePlayerBulletDamage(float damagePoints)
    {
        _damage += damagePoints;
        if (_damage < 1)
            _damage = 1f;
    }

    public void ChangePlayerFireRate(float fireRatePoints)
    {
        _fireRateDelay += fireRatePoints;
        if (_fireRateDelay <= 0)
            _fireRateDelay = 0.05f;
    }

    public void ChangePlayerSpread(float spreadDegreesPoints)
    {
        _spreadDegrees += spreadDegreesPoints;
        if (_spreadDegrees < 0)
            _fireRateDelay = 0;
    }

    public void ChangePlayerShootingForce(float shootingForcePoints)
    {
        _bulletLifeTime *= _shootingForce / (_shootingForce + shootingForcePoints);
        _shootingForce += shootingForcePoints;
        if (_shootingForce <= 0)
            _shootingForce = 1;
    }

    public void ChangeAdditionalBulletAmount(int  amount)
    {
        _additionalBullets += amount;
        if (_additionalBullets < 0)
            _additionalBullets = 0;
    }
}
