using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagement : MonoBehaviour
{
    public static UIManagement uiManagerInstance = null;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] public GameObject _healthBar;
    [SerializeField] private GameObject _abilityButton;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _movementJoystick;
    [SerializeField] private GameObject _shootingJoystick;
    [SerializeField] private GameObject _movementJoystickOutline;
    [SerializeField] private GameObject _shootingJoystickOutline;

    private void Awake()
    {
        if (uiManagerInstance == null)
            uiManagerInstance = this;
        else
            DestroyImmediate(this);
    }

    public void DisplayPauseUI(bool toPause)
    {
        SetActiveGameElements(!toPause);
        _pauseMenu.SetActive(toPause);
    }

    public void DisplayWinUI()
    {
        SetActiveGameElements(false);
        _pauseMenu.SetActive(false);
        _winMenu.SetActive(true);
    }

    public void DisplayGameOverUI()
    {
        SetActiveGameElements(false);
        _pauseMenu.SetActive(false);
        _gameOverMenu.SetActive(true);
    }

    public void SetActiveGameElements(bool active)
    {
        _pauseButton.SetActive(active);
        _healthBar.SetActive(active);
        _abilityButton.SetActive(active);
        _movementJoystick.SetActive(active);
        _shootingJoystick.SetActive(active);
        _movementJoystickOutline.SetActive(active);
        _shootingJoystickOutline.SetActive(active);
    }

    public void SetActiveMenuElements(bool active)
    {
        _pauseMenu.SetActive(active);
        _winMenu.SetActive(active);
        _gameOverMenu.SetActive(active);
    }
}
