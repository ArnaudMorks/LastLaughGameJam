using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float runSpeed = 0.6f; // Running speed.
   // [SerializeField] private float jumpForce = 2.6f; // Jump height.      WAARSCHIJNLIJK NIET NODIG

    private Rigidbody2D body; // Variable for the RigidBody2D component.
    //private Animator animator; // Variable for the Animator component. [OPTIONAL]
    [SerializeField] private GameObject myLocation;
    [SerializeField] private Transform myXLocation;
    private float myCurrentXLocation;


    [SerializeField] private float centerSide = 0;
    [SerializeField] private float leftSide = -5f;
    [SerializeField] private float rightSide = 5f;
    private bool goLeft;

    private bool goingLeft = false; // Variable that will check is "A" key is pressed.
    private bool goingRight = false; // Variable that will check is "D" key is pressed.

    void Start()
    {
        body = GetComponent<Rigidbody2D>(); // Setting the RigidBody2D component.
        //animator = GetComponent<Animator>(); // Setting the Animator component. [OPTIONAL]
    }

   








    // Update() is called every frame.
    void Update()
    {
        myCurrentXLocation = myXLocation.position.x;

        //ReturnToCenter();

        //    Debug.Log(goLeft);
        //Debug.Log(myCurrentXLocation);
        if (myCurrentXLocation < rightSide && goLeft == true)
        {

            goingRight = true; // Checking on "A" key pressed.
        }
        if (myCurrentXLocation > rightSide)
        {
            goLeft = false;
        }
        
        if (myCurrentXLocation > leftSide && goLeft == false)
        {

            goingLeft = true; // Checking on "A" key pressed.
        }
        
        if (myCurrentXLocation < leftSide)
        {
            goLeft = true;
        }
        

        /*
        if (myCurrentXLocation > rightSide)
        {
            Debug.Log("rightside");
        }
        if (goLeft == false)
        {
            Debug.Log("goleft");
        }
        */










        
        

        
    }
    void ReturnToCenter()
    {





        if (myCurrentXLocation >= (centerSide-1)|| myCurrentXLocation >= (centerSide + 1))
        {
            Debug.Log("im in center");
        }

        if (myCurrentXLocation >= centerSide)
        {
            goingLeft = true; // Checking on "A" key pressed.
        }
        if (myCurrentXLocation <= centerSide)
        {
            goingRight = true; // Checking on "A" key pressed.
        }


    }


    void FixedUpdate()
    {

        // Left/Right movement.
        if (goingLeft)
        {
            body.velocity = new Vector2(-runSpeed, body.velocity.y); // Move left physics.
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z); // Rotating the character object to the left.
            goingLeft = false; // Returning initial value.
        }
        else if (goingRight)
        {
            body.velocity = new Vector2(runSpeed, body.velocity.y); // Move right physics.
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z); // Rotating the character object to the right.
            goingRight = false; // Returning initial value.
        }
        else body.velocity = new Vector2(0, body.velocity.y);
    }





    //Invoke("JumpWings", 0.2f);








}
