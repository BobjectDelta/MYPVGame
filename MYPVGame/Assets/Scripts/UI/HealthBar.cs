using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _player;
    [SerializeField] private Health _health;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _health = _player.GetComponent<Health>();
        SetMaxValue(_health.GetHealth());
    }

    
    public void SetMaxValue(float healthPoints)
    {
        _slider.maxValue = healthPoints;
        _slider.value = healthPoints;
    }

    public void SetCurrentValue(float healthPoints)
    {
        _slider.value = healthPoints;
    }
}
