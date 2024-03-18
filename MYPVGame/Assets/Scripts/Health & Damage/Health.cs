using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private bool _isInvincible = false;
    [SerializeField] private float _invincibilityTime = 3f;

    private IEnumerator _invincibilitycoroutine;

    private void Start()
    {
        if (_health > _maxHealth)
            _health = _maxHealth;
    }
    public void TakeDamage(float damage)
    {
        Debug.Log(_isInvincible);
        if (!_isInvincible)
        {
            _health -= damage;
            if (gameObject.CompareTag("Player") && GameManagement.gameManagerInstance != null)
                UIManagement.uiManagerInstance._healthBar.GetComponent<HealthBar>().SetCurrentValue(_health);
            _invincibilitycoroutine = SetInvincibility(_invincibilityTime);
            StartCoroutine(_invincibilitycoroutine);
            if (_health <= 0)
            {
                _health = 0;
                Die();
                return;
            }
            Debug.Log(_health);
        }
        //else
            //Debug.Log("no dmg");
    }

    private void Die()
    {
        if (gameObject.CompareTag("Player") && GameManagement.gameManagerInstance != null)
            GameManagement.gameManagerInstance.GameOver();
        if (gameObject.GetComponent<DefaultEnemyAI>() != null)
            gameObject.GetComponent<DefaultEnemyAI>().DoBeforeDestruction();
        Destroy(gameObject);
    }

    public void Heal(float healAmount)
    {
        _health += healAmount;
        if (_health > _maxHealth)        
            _health = _maxHealth;
        if (gameObject.CompareTag("Player") && GameManagement.gameManagerInstance != null)
            UIManagement.uiManagerInstance._healthBar.GetComponent<HealthBar>().SetCurrentValue(_health);
        Debug.Log(_health);
    }

    public IEnumerator SetInvincibility(float invincibilityTime)
    {
        //Debug.Log("Coroutine started");

        _isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        _isInvincible = false;
        //Debug.Log("Coroutine ended");
    }

    public void StopInvincibilityCoroutine()
    {
        StopCoroutine(_invincibilitycoroutine);
        _isInvincible = false;
        //Debug.Log("Coroutine stopped");
    }

    public float GetHealth()
    {
        return _health;
    }

    public bool GetIsInvincible()
    {
        return _isInvincible;
    }
}
