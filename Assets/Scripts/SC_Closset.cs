using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Closset : MonoBehaviour
{
    [SerializeField] private GameObject playerSprites;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private Transform playerLocation;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private SpriteRenderer openDoorsSprite;

    private bool inCloset = false;
    private bool nearClosset = false;
    private bool inClossetDelay = false;
    private bool clossetSpammer = false;
    private bool EPressed = false;
    private float movementX;
    private float movementY;
    void Update()
    {

        if (nearClosset == true || inCloset == true)  // Get the keys to go into and get out of the closset.
        {
            if (Input.GetKey(KeyCode.E)) EPressed = true;
            movementX = Input.GetAxisRaw("Horizontal");
            movementY = Input.GetAxisRaw("Vertical");
        }

        if (nearClosset == true &&  clossetSpammer == false)  // If you are near the closset and press E or up you enter the closset.
        {
            if (EPressed == true || movementY == 1)
            {
                playerRb.simulated = false;      // Disables the gravity so it doesnt spike at 1000km/h and go through the floor.
                openDoorsSprite.enabled = false; // Disables the openDoorsSprite.
                playerSprites.SetActive(false);  // Disables the playerSprite.
                playerCollider.enabled = false;  // Disables the collider of the player.
                inCloset = true;                 // 
                inClossetDelay = false;          // 
                playerLocation.position = new Vector3(this.transform.position.x, (this.transform.position.y) - 0.3f, this.transform.position.z); // Sets the position of the player to the closset opon entering.
                Invoke("ClossetDelayerOut", 0.5f);  // Sets the delay for inClossetDelay so you cant spam it.
            }
        }
        if (movementX == 1 || movementX == -1 || EPressed == true || movementY == 1 || movementY == -1)     // Checks if you pressed up, down or e to get out.
        {
            if (inCloset == true && inClossetDelay == true)  // Checks if you are in the closset and if you waited for the delay.
            {
                playerRb.simulated = true;      // Enables the gravity again.
                openDoorsSprite.enabled = true; // Enables the openDoorsSprite.
                playerSprites.SetActive(true);  // Enables the playerSprite.
                playerCollider.enabled = true;  // Enables the collider of the player.
                playerLocation.position = new Vector3(this.transform.position.x, (this.transform.position.y), this.transform.position.z); // Sets the position of the player to the closset upon exiting.
                inCloset = false;
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
        EPressed = false;
    }
    void ClossetDelayerIn()
    {
        clossetSpammer = false;
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
