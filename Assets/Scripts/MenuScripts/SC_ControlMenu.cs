using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_ControlMenu : MonoBehaviour
{
    [SerializeField] private GameObject controlsMenu;
    private SC_GamePause gamePause;

    public void ShowControlMenu()
    {
        controlsMenu.SetActive(true);
        gamePause = FindObjectOfType<SC_GamePause>();
        gamePause.ControlsMenuIsOn = true;
    }
}
