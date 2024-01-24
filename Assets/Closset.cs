using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closset : MonoBehaviour
{
    public GameObject playerSprites;
    public Collider2D playerCollider;
    public Transform playerLocation;
    public Rigidbody2D playerRb;
    public CharackterController2D controller2D;
    public SpriteRenderer openDoorsSprite;

    private bool inCloset = false;
    private bool APressed = false;
    private bool DPressed = false;
    private bool EPressed = false;
    private bool nearClosset = false;
    private bool inClossetDelay = false;
    private bool clossetSpammer = false;
    void Update()
    {
        if (nearClosset == true || inCloset == true)  // Get the keys to go into and get out of the closset.
        {
            if (Input.GetKey(KeyCode.A)) APressed = true; // To get out of the closset.
            if (Input.GetKey(KeyCode.D)) DPressed = true; 
            if (Input.GetKey(KeyCode.E)) EPressed = true; // To go into the closset.
        }

        if (nearClosset == true && EPressed == true && clossetSpammer == false)  // If you are near the closset and press E you enter the closset.
        {
            playerRb.simulated = false;      // Disables the gravity so it doesnt spike at 1000km/h and go through the floor.
            openDoorsSprite.enabled = false; // Disables the openDoorsSprite.
            playerSprites.SetActive(false);  // Disables the playerSprite.
            playerCollider.enabled = false;  // Disables the collider of the player.
            inCloset = true;                 // 
            inClossetDelay = false;          // 
            Invoke("ClossetDelayerOut", 0.5f);  // Sets the delay for inClossetDelay so you cant spam it.
        }
        if (DPressed == true || APressed == true || EPressed == true)     // Checks if you pressed a,d or e to get out.
        {
            if (inCloset == true && inClossetDelay == true)  // Checks if you are in the closset and if you waited for the delay.
            {
                playerRb.simulated = true;      // Enables the gravity again.
                openDoorsSprite.enabled = true; // Enables the openDoorsSprite.
                playerSprites.SetActive(true);  // Enables the playerSprite.
                playerCollider.enabled = true;  // Enables the collider of the player.
                playerLocation.position = new Vector3(this.transform.position.x, (this.transform.position.y), this.transform.position.z); // Sets the position of the player to the closset upon exiting.
                inCloset = false;
                DPressed = false;
                APressed = false;
                EPressed = false;
                clossetSpammer = true;
                Invoke("ClossetDelayerIn", 0.5f);  // Sets the delay for clossetSpammer so you cant spam it.
            }
        }
    }
    // Adds a delay to get in and when getting out so you cant spam it.
    void ClossetDelayerOut()
    {
        inClossetDelay = true;
        DPressed = false;
        APressed = false;
        EPressed = false;
    }
    void ClossetDelayerIn()
    {
        clossetSpammer = false;
        DPressed = false;
        APressed = false;
        EPressed = false;
    }
    // If the player enters the trigger of the closset set nearClosset to true and when the player leaves back to false.
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
