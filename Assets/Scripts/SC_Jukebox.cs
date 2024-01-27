using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Jukebox : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D playerCollider;
    [SerializeField] private BoxCollider2D doorTrigger;
    [SerializeField] private AudioSource audioSource;

    private bool playerInRange = false;
    private bool jukeBoxOn = false;
    public bool JukeBoxOn        //zodat andere scripts bij de variabele kunnen die hier in staat   Gebruik zodat Enemy erbij kan
    {
        get { return jukeBoxOn; }
        set { jukeBoxOn = value; }
    }

    void Start()
    {
        playerCollider = FindObjectOfType<SC_CharacterController2D>().GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if (Time.timeScale == 0)        //dat je niet op konppen kan klikken als het spel op pauze staat
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) && playerInRange == true)
        {
            JukeBoxClick();
            print("clicked E");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerCollider != null)     //kan iets doen als de "playerCollider" niet niks is; als de "playerCollider" in het trigger veld is is dit niet "null"
        {
            playerInRange = true;
            print(playerInRange);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (playerCollider != null)
        {
            playerInRange = false;
            print(playerInRange);
        }
    }

    private void JukeBoxClick()
    {
        if (jukeBoxOn == false)
        {
            audioSource.Play();
            jukeBoxOn = true;
        }
        else 
        {
            audioSource.Pause();
            jukeBoxOn = false;
        }
    }

}
