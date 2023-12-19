using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private GameObject _hitEffect;
    private float _damage;

    private void OnTriggerEnter2D(Collider2D collisionObject)
    {
        if (!collisionObject.CompareTag("Player") && collisionObject.GetComponent<PlayerBullet>() == null)
        {
            Health health = collisionObject.GetComponent<Health>();
            if (health != null)
                health.TakeDamage(_damage);
            Debug.Log(health);

            Destroy(gameObject);
        }
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }
}
