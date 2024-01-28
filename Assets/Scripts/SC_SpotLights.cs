using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SpotLights : MonoBehaviour
{
    private SC_GamePause gamePause;
    private GameObject spotLight;
    public SC_HidingChecker hidin;
    public float rotationSpeed = 10f;
    public bool goRight = false;
    public bool pauseABit = true;
    public bool iWaited = true;
    public bool iWaited2 = true;
    void Start()
    {
        gamePause = FindObjectOfType<SC_GamePause>();
        pauseABit = true;
        iWaited2 = true;
        iWaited = true;
    }
    void Update()
    {
        VanAchterNaarVoren();
        CanISeePlayer();
    }

    public bool inSpotlight = false;
    public bool hidingBehing = false;
    
    void CanISeePlayer()
    {
        if (inSpotlight)
        {
            if (hidin.imHidden == false) // Let it seperate or its gonna call every frame even when player is now visible
            {
                Debug.Log("see u");
                gamePause.playerDead = true;
            }
        }

    }


    private void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if (collisionInfo.CompareTag("Player"))
        {
            inSpotlight = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inSpotlight = false;
        }
    }


    private void VanAchterNaarVoren()
    {
        if (goRight && iWaited2)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        if (goRight == false && iWaited2)
        {
            transform.Rotate(Vector3.forward, -(rotationSpeed * Time.deltaTime));
        }
        if (pauseABit)
        {
            pauseABit = false;
            Invoke("PauseCalc", 2f);
        }
    }
    void PauseCalc()
    {
        goRight = true;
        iWaited = true;
        Invoke("PauseCalc2", 11f);
    }
    void PauseCalc2()
    {
        goRight = false;
        iWaited = true;
        Invoke("PauseCalc", 11f);
    }
}
