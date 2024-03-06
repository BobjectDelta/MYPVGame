using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static GameManagement gameManagerInstance = null;
    [SerializeField] public GameObject player = null;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] public GameObject _healthBar;
    [SerializeField] private GameObject _abilityButton;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _movementJoystick;
    [SerializeField] private GameObject _shootingJoystick;


    private int _enemiesToDefeat = 0;
    private bool _isPaused = false;

    private void Awake()
    {
        if (gameManagerInstance == null)
            gameManagerInstance = this;
        else
            DestroyImmediate(this);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Time.timeScale = 1;
        //player.GetComponent<ShootingInput>().SetShootingJoystick(_shootingJoystick);
        //player.GetComponent<PlayerMovement>().SetMovementJoystick(_movementJoystick);
        SetActiveMenuElements(false);
        SetActiveGameElements(true);
        SetInitialEnemyCount();
    }

    private void SetInitialEnemyCount()
    {
        _enemiesToDefeat = FindObjectsOfType<DefaultEnemyAI>().Length;
    }

    public void DecrementEnemiesToDefeat()
    {
        _enemiesToDefeat--;
        if (_enemiesToDefeat <= 0)
            Win();
    }

    public void TogglePause()
    {
        if (!_isPaused)
            Pause();
        else
            Resume();
    }

    private void Pause()
    {
        SetActiveGameElements(false);
        Time.timeScale = 0;
        _pauseMenu.SetActive(true);
        _isPaused = true;
    }

    private void Resume()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
        _isPaused = false;
        SetActiveGameElements(true);
    }

    private void Win()
    {
        player.SetActive(false);
        SetActiveGameElements(false);
        _winMenu.SetActive(true);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        _pauseMenu.SetActive(false);
        SetActiveGameElements(false);
        _gameOverMenu.SetActive(true);
    }

    public void SetActiveGameElements(bool active)
    {
        _pauseButton.SetActive(active);
        _healthBar.SetActive(active);
        _abilityButton.SetActive(active);
        _movementJoystick.SetActive(active);
        _shootingJoystick.SetActive(active);
    }

    public void SetActiveMenuElements(bool active)
    {
        _pauseMenu.SetActive(active);
        _winMenu.SetActive(active);
        _gameOverMenu.SetActive(active);
    }
}
