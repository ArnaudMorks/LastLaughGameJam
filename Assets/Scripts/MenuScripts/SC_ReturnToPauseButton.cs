using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_ReturnToPauseButton : MonoBehaviour
{
    [SerializeField] private GameObject controlsMenu;
    private SC_GamePause gamePause;

    public void ReturnToPauseMenu()
    {
        gamePause = FindObjectOfType<SC_GamePause>();
        gamePause.ControlsMenuIsOn = false;
        controlsMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ReturnToPauseMenu();            //als op pauze klikt terwijl controll scherm aan staat gaat die terug naar standaard pauze menu
        }
    }

}
