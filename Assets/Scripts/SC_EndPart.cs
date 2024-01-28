using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_EndPart : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {


        SC_CharacterController2D player = collision.gameObject.GetComponent<SC_CharacterController2D>();

        if (player != null && collision.isTrigger == false)
        {
            PlayerOnFinish();
        }
    }

    public void PlayerOnFinish()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
