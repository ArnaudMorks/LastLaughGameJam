using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Door : MonoBehaviour
{

    [SerializeField] private CapsuleCollider2D playerCollider;
    [SerializeField] private BoxCollider2D doorCollider;
    [SerializeField] private BoxCollider2D doorTrigger;
    [SerializeField] private Sprite openDoorsSprite;
    [SerializeField] private Sprite closedDoorSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private bool isOpen = false;       //hiermee kan je ook bepalen of die open of dicht moet beginnen
    private bool playerInRange = false;

    [SerializeField] private AudioClip audioOpen;
    [SerializeField] private AudioClip audioClose;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        playerCollider = FindObjectOfType<SC_CharacterController2D>().GetComponent<CapsuleCollider2D>();

        if (isOpen == true)
        {
            spriteRenderer.sprite = openDoorsSprite;
            doorCollider.enabled = false;
            isOpen = true;
        }
        else
        {
            spriteRenderer.sprite = closedDoorSprite;           //zelfde code als bij Open en Close functies maar dan zonder geluid
            doorCollider.enabled = true;
            isOpen = false;
        }
    }


    void Update()
    {
            if (Input.GetKeyDown(KeyCode.E) && playerInRange == true)
            {
                if (isOpen == true) { DoorClose(); }
                else { DoorOpen(); }
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerCollider != null)     //kan iets doen als de "playerCollider" niet niks is; als de "playerCollider" in het trigger veld is is dit niet "null"
        {
            playerInRange = true;
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


    private void DoorOpen()
    {
        spriteRenderer.sprite = openDoorsSprite;
        doorCollider.enabled = false;
        audioSource.clip = audioOpen;
        audioSource.volume = Random.Range(0.8f, 1);
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(audioSource.clip);
        isOpen = true;
    }

    private void DoorClose()
    {
        spriteRenderer.sprite = closedDoorSprite;
        doorCollider.enabled = true;
        audioSource.clip = audioClose;
        audioSource.volume = Random.Range(0.8f, 1);
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(audioSource.clip);
        isOpen = false;
    }

}
