using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityMine : MonoBehaviour
{
    [SerializeField] private float _damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, transform.gameObject.GetComponent<CircleCollider2D>().radius+1);
        foreach (Collider2D collider in colliders)
            if (collider.CompareTag("Enemy") && collider.TryGetComponent<Health>(out Health health))
            {
                health.TakeDamage(_damage);
                //
                Destroy(gameObject);
            }                    
    }
}
