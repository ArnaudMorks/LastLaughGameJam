using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject myLocation;
    [SerializeField] private Transform myXLocation;
    [SerializeField] private Transform playerLocation;
    [SerializeField] private float leftOfMe;
    [SerializeField] private float rightOfMe;
    private Rigidbody2D body;
    [SerializeField] private float runSpeed = 0.6f;
    private float myCurrentXLocation;
    private float leftSide;
    private float rightSide;
    private bool goLeft;
    private bool goingLeft = false;
    private bool goingRight = false;
    private bool patrol = true;
    private bool playerSpotted = false;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        // Calculate the left- and right-side of the enemy
        leftSide = this.transform.position.x - leftOfMe;
        rightSide = this.transform.position.x + rightOfMe;
        playerLocation = FindObjectOfType<SC_CharacterController2D>().transform;
    }

    void Update()
    {
        myCurrentXLocation = myXLocation.position.x;
        if (playerSpotted == true)
        {
            if (myCurrentXLocation >= playerLocation.position.x)
            {
                goingLeft = true;
            }
            if (myCurrentXLocation <= playerLocation.position.x)
            {
                goingRight = true;
            }

        }
        if (patrol == true && playerSpotted == false)
        {
            if (myCurrentXLocation < rightSide && goLeft == true)
            {
                goingRight = true;
            }
            if (myCurrentXLocation > rightSide)
            {
                goLeft = false;
            }
            if (myCurrentXLocation > leftSide && goLeft == false)
            {
                goingLeft = true;
            }
            if (myCurrentXLocation < leftSide)
            {
                goLeft = true;
            }
        }
    }
    void FixedUpdate()
    {
        // Movement of the enemy.
        if (goingLeft)
        {
            body.velocity = new Vector2(-runSpeed, body.velocity.y);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z); 
            goingLeft = false;
        }
        else if (goingRight)
        {
            body.velocity = new Vector2(runSpeed, body.velocity.y);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
            goingRight = false;
        }
        else body.velocity = new Vector2(0, body.velocity.y);
    }
    // Detects if the player enters the trigger.
    private void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if (collisionInfo.CompareTag("Player"))
        {
            playerSpotted = true;
            runSpeed = 2.6f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerSpotted = false;
            runSpeed = 1.6f;
        }
    }
}
