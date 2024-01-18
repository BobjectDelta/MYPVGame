using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    ToggleGroup toggleGroup;
    private Toggle[] _toggles;

    private void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();
        _toggles = toggleGroup.GetComponentsInChildren<Toggle>();
    }

    public void RefreshToggles()
    {
        foreach (Toggle toggle in _toggles) 
            toggle.isOn = false;
        _toggles[0].isOn = true;
    }

    public void SaveCharacterIndex()
    {       
            for (int i = 0; i < _toggles.Length; i++)
                if (_toggles[i].isOn)
                    PlayerPrefs.SetInt("selectedCharacterIndex", i);       
    }
}
