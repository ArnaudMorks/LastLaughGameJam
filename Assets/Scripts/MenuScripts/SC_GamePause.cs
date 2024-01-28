using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SC_GamePause : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public string currentLevel;     //is "public" omdat het in andere scripts wordt opgeroepen; wordt NIET in andere scripts verandert
    [SerializeField] private GameObject deathMenu;
    private bool controlsMenuIsOn = false;
    private SC_CharacterController2D player;
    public bool playerDead = false;
    public bool ControlsMenuIsOn        //zodat andere scripts bij de variabele kunnen die hier in staat
    {
        get { return controlsMenuIsOn; }
        set { controlsMenuIsOn = value; }
    }

    void Start()
    {
        playerDead = false;
        Time.timeScale = 1;
        Scene currentScene = SceneManager.GetActiveScene(); //haalt huidige scene op
        currentLevel = currentScene.name;           //zet de huidige scene op tot een "string"
      //  Time.timeScale = 0;       //Decomment als je op pauze wilt beginnen
    }


    void Update()
    {
        if (playerDead)
        {
            Time.timeScale = 0;
            deathMenu.SetActive(true);
            return;
        }

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

    public void GoToMainMenu()
    {
        PauseResumeGame();      //zorgt dat die niet op pauze is als je weer terug naar het spel gaat
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        deathMenu.SetActive(false);
        Time.timeScale = 1;

        SceneManager.LoadScene(currentLevel);       //herlaad het huidige level

    }


}
