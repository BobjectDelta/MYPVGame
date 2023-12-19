using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooting : MonoBehaviour
{
    [SerializeField] protected Transform _bulletSpawnPoint;
    [SerializeField] protected GameObject _bulletPrefab;
    [SerializeField] protected GameObject _impactEffect;

    [SerializeField] protected float _spreadDegrees;
    [SerializeField] protected float _damage = 1f;
    [SerializeField] protected float _fireRateDelay;

    protected bool _canShoot = true;

    public virtual void Shoot() { }

    public IEnumerator SetShootAbility()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_fireRateDelay);
        _canShoot = true;
    }

    public virtual void ChangePlayerBulletDamage(float damagePoints) { }

    public virtual void ChangePlayerFireRate(float fireRatePoints) { }

    public virtual void ChangePlayerSpread(float spreadDegreesPoints) { }

    public virtual void ChangePlayerShootingForce(float shootingForcePoints) { }

    public virtual void ChangeAdditionalBulletAmount(int amount) { }
}
