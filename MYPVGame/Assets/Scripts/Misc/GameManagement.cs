using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static GameManagement gameManagerInstance = null;
    [SerializeField] public GameObject player = null;

    private UIManagement _uiManager;


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

        _uiManager = UIManagement.uiManagerInstance;
        _uiManager.SetActiveMenuElements(false);
        _uiManager.SetActiveGameElements(true);

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
        Time.timeScale = 0;
        _isPaused = true;
        _uiManager.DisplayPauseUI(_isPaused);
    }

    private void Resume()
    {
        Time.timeScale = 1;
        _isPaused = false;
        _uiManager.DisplayPauseUI(_isPaused);
    }

    private void Win()
    {
        Time.timeScale = 0;
        _uiManager.DisplayWinUI();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        _uiManager.DisplayGameOverUI();
    }

}
