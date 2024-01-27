using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Closet : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private Transform playerLocation;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private Sprite openDoorsSprite;
    [SerializeField] private Sprite closedDoorSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool inCloset = false;
    private bool nearCloset = false;
    private bool inClosetDelay = false;
    private bool closetSpammer = false;
    private bool EPressed = false;
    private float movementX;
    private float movementY;

    [SerializeField] private AudioClip audioOpen;
    [SerializeField] private AudioClip audioClose;
    [SerializeField] private AudioSource audioSource;


    private void Start()
    {
        playerLocation = FindObjectOfType<SC_CharacterController2D>().transform;
        playerCollider = FindObjectOfType<SC_CharacterController2D>().GetComponent<CapsuleCollider2D>();
        playerRb = FindObjectOfType<SC_CharacterController2D>().GetComponent<Rigidbody2D>();
        playerGameObject = FindObjectOfType<SC_CharacterController2D>().gameObject;
        spriteRenderer.sprite = openDoorsSprite;
    }

    void Update()
    {

        if (nearCloset == true || inCloset == true)  // Get the keys to go into and get out of the closset.
        {
            if (Input.GetKey(KeyCode.E)) EPressed = true;
            movementX = Input.GetAxisRaw("Horizontal");
            movementY = Input.GetAxisRaw("Vertical");
        }

        if (nearCloset == true &&  closetSpammer == false)  // If you are near the closset and press E or up you enter the closset.
        {
            if (EPressed == true || movementY == 1)
            {
                playerRb.simulated = false;      // Disables the gravity so it doesnt spike at 1000km/h and go through the floor.
                                                 //  openDoorsSprite.enabled = false; // Disables the openDoorsSprite.
                spriteRenderer.sprite = closedDoorSprite;
                playerGameObject.SetActive(false);  // Disables the playerSprite.
                playerCollider.enabled = false;  // Disables the collider of the player.
                inCloset = true;                 // 
                inClosetDelay = false;          // 
                playerLocation.position = new Vector3(this.transform.position.x, (this.transform.position.y) - 0.3f, this.transform.position.z); // Sets the position of the player to the closset opon entering.

                audioSource.clip = audioClose;
                audioSource.volume = Random.Range(0.8f, 1);
                audioSource.pitch = Random.Range(0.8f, 1.2f);
                audioSource.PlayOneShot(audioSource.clip);

                Invoke("ClosetDelayerOut", 0.5f);  // Sets the delay for inClossetDelay so you cant spam it.
            }
        }
        if (movementX != 0 || EPressed == true || movementY != 0)     // Checks if you pressed up, down or e to get out.
        {
            if (inCloset == true && inClosetDelay == true)  // Checks if you are in the closset and if you waited for the delay.
            {
                playerRb.simulated = true;      // Enables the gravity again.
                                                // openDoorsSprite.enabled = true; // Enables the openDoorsSprite.
                spriteRenderer.sprite = openDoorsSprite;
                playerGameObject.SetActive(true);  // Enables the playerSprite.
                playerCollider.enabled = true;  // Enables the collider of the player.
                playerLocation.position = new Vector3(this.transform.position.x, (this.transform.position.y), this.transform.position.z); // Sets the position of the player to the closset upon exiting.
                inCloset = false;
                EPressed = false;
                closetSpammer = true;

                audioSource.clip = audioOpen;
                audioSource.volume = Random.Range(0.8f, 1);
                audioSource.pitch = Random.Range(0.8f, 1.2f);
                audioSource.PlayOneShot(audioSource.clip);

                Invoke("ClosetDelayerIn", 0.5f);  // Sets the delay for clossetSpammer so you cant spam it.
            }
        }
    }
    // Adds a delay to get in and when getting out so you cant spam it.
    void ClosetDelayerOut()
    {
        inClosetDelay = true;
        EPressed = false;
    }
    void ClosetDelayerIn()
    {
        closetSpammer = false;
        EPressed = false;
    }
    // If the player enters the trigger of the closset set nearClosset to true and when the player leaves back to false.
    private void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if (collisionInfo.CompareTag("Player"))
        {
            nearCloset = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            nearCloset = false;
        }
    }
}
