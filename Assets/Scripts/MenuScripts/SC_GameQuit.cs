using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_QuitGame : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QuitGame");
    }
}
