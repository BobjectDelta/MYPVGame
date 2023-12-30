using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    ToggleGroup toggleGroup;

    private void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();
    }

    public void SaveCharacterIndex()
    {       
            Toggle[] toggles = toggleGroup.GetComponentsInChildren<Toggle>();
            for (int i = 0; i < toggles.Length; i++)
                if (toggles[i].isOn)
                    PlayerPrefs.SetInt("selectedCharacterIndex", i);       
    }
}
