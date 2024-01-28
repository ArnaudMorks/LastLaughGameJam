using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_LoadLevel : MonoBehaviour
{
    [SerializeField] private GameObject levelSelectScreen;
    private bool levelSelectOn = false;
    public void LevelSelection()
    {
        if (levelSelectOn == false)
        {
            levelSelectScreen.SetActive(true);
            levelSelectOn = true;
        }
        else
        {
            levelSelectScreen.SetActive(false);
            levelSelectOn = false;
        }
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    /*    public void Level1()
        {
            SceneManager.LoadScene("Level01");
        }*/


    public void Level2()
    {
        SceneManager.LoadScene("BeachStorm");
    }

    public void Level3()
    {
        SceneManager.LoadScene("IndoorsTest");
    }


}
