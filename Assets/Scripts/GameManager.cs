using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // A static reference to the GameManager instance
    
    //Variables
    
    public InputManager _inputManager;
    public LevelManagerUI _LevelManager;
    public int _moveCount = 8;
    public int _totalCarsCount;
    public int _levelNumber = 1;
    
    public event Action OnGameOver;
    public event Action OnLevelCleared;
    void Awake()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else if(Instance != this) 
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        OnGameOver += GameOver;
        OnLevelCleared += LevelCleared;
    }

    // Start is called before the first frame update
    void Start()
    {
        OnLevelStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOverEvent()
    {
        OnGameOver.Invoke();
    }
    
    void GameOver()
    {
        _LevelManager._restartPanel.SetActive(true);
    }

    public void OnLevelStart()
    {

        _totalCarsCount = FindObjectsByType<CarController>(FindObjectsSortMode.None).Length;
        _moveCount = _totalCarsCount + 3;
        if(_LevelManager == null)
            _LevelManager = FindObjectOfType<LevelManagerUI>();
        _LevelManager._moveCountText.text = GameManager.Instance._moveCount.ToString("00");
    }

    public void LevelCleardEvent()
    {
        OnLevelCleared.Invoke();
    }

    void LevelCleared()
    {
        _LevelManager._levelPassedPanel.SetActive(true);
    }
}

