using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float _damage;

    private void OnCollisionEnter2D(Collision2D collisionObject)
    {
        if (collisionObject.collider.CompareTag("Player"))
            collisionObject.gameObject.GetComponent<Health>().TakeDamage(_damage);
    }

}
