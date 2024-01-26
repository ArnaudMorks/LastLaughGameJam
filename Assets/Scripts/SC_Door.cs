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


    void Start()
    {
        playerCollider = FindObjectOfType<SC_CharacterController2D>().GetComponent<CapsuleCollider2D>();

        if (isOpen == true) { DoorOpen(); }
        else { DoorOpen(); }
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
        isOpen = true;
    }

    private void DoorClose()
    {
        spriteRenderer.sprite = closedDoorSprite;
        doorCollider.enabled = true;
        isOpen = false;
    }

}
