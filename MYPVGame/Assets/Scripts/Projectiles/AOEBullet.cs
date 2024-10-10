using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEBullet : Bullet
{
    [SerializeField] private float _AOEradius = 2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (!collision.CompareTag(_ignoreTag.ToString()) && collision.GetComponent<Bullet>() == null)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, transform.gameObject.GetComponent<CircleCollider2D>().radius + _AOEradius);
                foreach (Collider2D collider in colliders)
                {
                    Debug.Log(collider.name);
                    if (!collider.CompareTag(_ignoreTag.ToString()))
                    {
                        Health health = collider.GetComponent<Health>();
                        if (health != null)
                            health.TakeDamage(_damage);
                    }
                }
            if (_hitEffect != null)
                Instantiate(_hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            }
    }
}
