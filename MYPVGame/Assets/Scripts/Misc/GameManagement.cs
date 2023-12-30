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

    private int _enemiesToDefeat;
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
        _pauseMenu.SetActive(false);
        _winMenu.SetActive(false);
        _gameOverMenu.SetActive(false);
        _pauseButton.SetActive(true);
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
        _pauseButton.SetActive(false);
        _abilityButton.SetActive(false);
        Time.timeScale = 0;
        _pauseMenu.SetActive(true);
        _isPaused = true;
    }

    private void Resume()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
        _isPaused = false;
        _pauseButton.SetActive(true);
        _abilityButton.SetActive(true);
    }

    private void Win()
    {
        player.SetActive(false);
        _pauseButton.SetActive(false);
        _abilityButton.SetActive(false);
        _pauseMenu.SetActive(false);
        _winMenu.SetActive(true);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        _pauseMenu.SetActive(false);
        _pauseButton.SetActive(false);
        _abilityButton.SetActive(false);
        _gameOverMenu.SetActive(true);
    }

}
