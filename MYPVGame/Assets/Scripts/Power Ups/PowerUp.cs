using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    private GameObject _powerUpRecipient;

    [SerializeField] protected float _effectTime;
    protected float _remainingEffectTime = 0;

    [SerializeField] protected bool _isTimed = false;
    [SerializeField] protected bool _isOverTime = false;
    private bool _isBuffed;


    private void Start()
    {
        _isBuffed = false;
    }

    private void Update()
    {
        if (_remainingEffectTime > 0)
        {                     
            if (!_isBuffed || _isOverTime)
                GrantPowerUp(_powerUpRecipient);
            _isBuffed = true;
            _remainingEffectTime -= Time.deltaTime;                  
        }
        else if (_isBuffed && _remainingEffectTime < 0)
        {
            RevertPowerUp(_powerUpRecipient);
            _isBuffed = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collisionObject)
    {
        if (collisionObject.CompareTag("Player"))
        {
            _powerUpRecipient = collisionObject.gameObject;
            if (!_isTimed)
                GrantPowerUp(_powerUpRecipient);
            else
                _remainingEffectTime = _effectTime;      
            Debug.Log(_remainingEffectTime);
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, _effectTime + 1);
        }
    }

    protected virtual void GrantPowerUp(GameObject powerUpRecipient) { }
    protected virtual void RevertPowerUp(GameObject powerUpRecipient) { }

    public virtual void SetEffectTime(float time)
    {
        _effectTime = time;
    }


}
