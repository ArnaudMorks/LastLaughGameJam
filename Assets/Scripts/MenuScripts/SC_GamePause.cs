using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SC_GamePause : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public string currentLevel;     //is "public" omdat het in andere scripts wordt opgeroepen; wordt NIET in andere scripts verandert
    private bool controlsMenuIsOn = false;
    public bool ControlsMenuIsOn        //zodat andere scripts bij de variabele kunnen die hier in staat
    {
        get { return controlsMenuIsOn; }
        set { controlsMenuIsOn = value; }
    }

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene(); //haalt huidige scene op
        currentLevel = currentScene.name;           //zet de huidige scene op tot een "string"
      //  Time.timeScale = 0;       //Decomment als je op pauze wilt beginnen
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && controlsMenuIsOn == false)
        {
            PauseResumeGame();
        }
    }

    public void PauseResumeGame()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(currentLevel);       //herlaad het huidige level

    }
}
