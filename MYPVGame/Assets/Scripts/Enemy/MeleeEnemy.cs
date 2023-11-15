using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float _damage;
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collisionObject)
    {
        if (collisionObject.collider.CompareTag("Player"))
            collisionObject.gameObject.GetComponent<Health>().TakeDamage(_damage);
    }

}
