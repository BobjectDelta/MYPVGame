using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityInput : MonoBehaviour
{
    [SerializeField] AbstractAbility _ability;
    [SerializeField] private Button _abilityButton;

    private void Start()
    {
        _abilityButton = GameObject.Find("AbilityButton").GetComponent<Button>();
        _abilityButton.onClick.AddListener(InputActivateAbility);
    }
    public void InputActivateAbility()
    {
        _ability.ActivateAbility();
    }   
}
