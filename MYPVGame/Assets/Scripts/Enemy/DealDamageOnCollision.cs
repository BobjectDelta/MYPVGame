using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageOnCollision : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _knockbackForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.gameObject.GetComponent<Health>().TakeDamage(_damage);
        //DealKnockback(collision);
    }

    /*private void DealKnockback(Collider2D collision)
    {
        Vector2 knockbackVector = (this.transform.position - collision.attachedRigidbody.transform.position).normalized;
        this.GetComponent<Rigidbody2D>().AddForce(knockbackVector * _knockbackForce, ForceMode2D.Impulse);
    }*/
}
