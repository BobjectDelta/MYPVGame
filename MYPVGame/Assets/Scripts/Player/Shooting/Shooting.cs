using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooting : MonoBehaviour
{
    [SerializeField] protected List<Transform> _bulletSpawnPoints;
    [SerializeField] protected GameObject _soundEffect;
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

    public virtual void ChangeBulletDamage(float damagePoints) { }

    public virtual void ChangeFireRate(float fireRatePoints) { }

    public virtual void ChangeShootingSpread(float spreadDegreesPoints) { }

    public virtual void ChangeShootingForce(float shootingForcePoints) { }

    public virtual void AddAdditionalBullet(int amount) { }

    public virtual void RemoveAdditionalBullet(int amount) { }
}
