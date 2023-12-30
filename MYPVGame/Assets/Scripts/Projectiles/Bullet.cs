using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _hitEffect;
    [SerializeField] private IgnoreTag _ignoreTag = IgnoreTag.Player;
    private float _damage;

    private void OnTriggerEnter2D(Collider2D collisionObject)
    {
        if (!collisionObject.CompareTag(_ignoreTag.ToString()) && collisionObject.GetComponent<Bullet>() == null)
        {
            Health health = collisionObject.GetComponent<Health>();
            if (health != null)
                health.TakeDamage(_damage);
            if (_hitEffect != null)
                Instantiate(_hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    private enum IgnoreTag
    {
        Player,
        Enemy
    }
}
