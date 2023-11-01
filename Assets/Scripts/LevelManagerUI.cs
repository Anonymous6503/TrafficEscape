using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerUI : MonoBehaviour
{
    public GameObject _restartPanel;
    public GameObject _levelPassedPanel;

    public TMP_Text _moveCountText;
    
    // Start is called before the first frame update
    void Start()
    {
        _restartPanel.SetActive(false);
        _levelPassedPanel.SetActive(false);
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRestartPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnExitPressed()
    {
        Application.Quit();
    }

    public void OnMainMenuPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnNextPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        GameManager.Instance.OnLevelStart();
    }
}
