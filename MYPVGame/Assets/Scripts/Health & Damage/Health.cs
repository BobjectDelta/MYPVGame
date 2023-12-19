using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    private bool _isInvincible = false;
    [SerializeField] private float _invincibilityTime = 3f;

    private void Start()
    {
        if (_health > _maxHealth)
            _health = _maxHealth;
    }
    public void TakeDamage(float damage)
    {
        if (!_isInvincible)
        {
            _health -= damage;
            StartCoroutine(SetInvincibility(_invincibilityTime));
            if (_health <= 0)
            {
                _health = 0;
                Die();
                return;
            }
            Debug.Log(_health);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void Heal(float healAmount)
    {
        _health += healAmount;
        if (_health > _maxHealth)        
            _health = _maxHealth;
        Debug.Log(_health);
    }

    public IEnumerator SetInvincibility(float  invincibilityTime)
    {
        _isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        _isInvincible = false;
    }

    public float GetHealth()
    {
        return _health;
    }
}
