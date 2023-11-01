using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private Button _startGame;
    [SerializeField] private Button _exitButton;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartPressed()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnExitPressed()
    {
        Application.Quit();
    }


}
