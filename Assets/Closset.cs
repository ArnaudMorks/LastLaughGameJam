using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closset : MonoBehaviour
{
    public GameObject playerSprites;
    public Collider2D playerCollider;
    public Transform playerLocation;
    public CharackterController2D controller2D;
    public SpriteRenderer openDoorsSprite;


    private bool inCloset = false;
    private bool APressed = false;
    private bool DPressed = false;
    private bool EPressed = false;
    private bool nearClosset = false;
    private bool inClossetDelay = false;

    // Update is called once per frame
    void Update()
    {
        if (nearClosset == true || inCloset == true)  // Get the keys to go into and get out of the closset.
        {
            if (Input.GetKey(KeyCode.A)) APressed = true; // To get out of the closset.
            if (Input.GetKey(KeyCode.D)) DPressed = true; 
            if (Input.GetKey(KeyCode.E)) EPressed = true; // To go into the closset.
        }

        if (nearClosset == true && EPressed == true)  // If you are near the closset and press E you enter the closset.
        {
            openDoorsSprite.enabled = false; // Disables the openDoorsSprite.
            playerSprites.SetActive(false);  // Disables the playerSprite.
            playerCollider.enabled = false;  // Disables the collider of the player.
            inCloset = true;                 // 
            inClossetDelay = false;          // 
            Invoke("ClossetDelayer", 0.5f);  // Sets the delay for inClossetDelay so you cant spam it.
        }
        if (DPressed == true || APressed == true)  
        {
            if (inCloset == true && inClossetDelay == true)
            {
                openDoorsSprite.enabled = true;
                playerSprites.SetActive(true);
                playerCollider.enabled = true;
                playerLocation.position = new Vector3(this.transform.position.x, (this.transform.position.y + 0.2f), this.transform.position.z);
                inCloset = false;
                DPressed = false;
                APressed = false;
                EPressed = false;
            }
        }
    }
    void ClossetDelayer()
    {
        inClossetDelay = true;
        DPressed = false;
        APressed = false;
        EPressed = false;
    }

    
    private void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if (collisionInfo.CompareTag("Player"))
        {
            nearClosset = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            nearClosset = false;
        }

    }
}
