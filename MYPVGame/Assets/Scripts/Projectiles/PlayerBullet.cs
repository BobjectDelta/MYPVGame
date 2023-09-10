using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private GameObject _hitEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //GameObject effect = Instantiate(_hitEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 2f);
        Destroy(gameObject);
    }
}
