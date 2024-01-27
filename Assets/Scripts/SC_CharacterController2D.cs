using UnityEngine;

public class SC_CharacterController2D : MonoBehaviour
{
    [SerializeField] private float runSpeedBase;    //standaard loopsnelheid (als je gewoon staat)
    [SerializeField] private float runSpeedBaseChrouched;
    [SerializeField] private float runSpeed; // Running speed.
    [SerializeField] private float jumpForce = 2.6f; // Jump height.

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite standingSprite;
    [SerializeField] private Sprite chrouchingSprite;
    [SerializeField] private Sprite jumpSprite; // Sprite that shows up when the character is not on the ground. [OPTIONAL]

    private Rigidbody2D body; // Variable for the RigidBody2D component.
    //private SpriteRenderer sr; // Variable for the SpriteRenderer component.
    private Animator animator; // Variable for the Animator component. [OPTIONAL]
    //public Transform transform;

    private bool canLand = false;     //voor als die geland is
    private bool isGrounded; // Variable that will check if character is on the ground.
    [SerializeField] private GameObject groundCheckPoint; // The object through which the isGrounded check is performed.
    [SerializeField] private float groundCheckRadius; // isGrounded check radius.
    [SerializeField] private LayerMask groundLayer; // Layer wich the character can jump on.

    [SerializeField] private float jumpCoolDownBase = 0.2f;
    [SerializeField] private float jumpCoolDown;
    private bool canJump = true;

/*    [SerializeField] private bool APressed = false; // Variable that will check is "A" key is pressed.
    [SerializeField] private bool DPressed = false; // Variable that will check is "D" key is pressed.
    [SerializeField] private bool EPressed = false; // Variable that will check is "E" key is pressed.*/
    private float movementX;
    private float runSpeedDirection;

    private bool chrouchModeOn = false;
    [SerializeField] private CapsuleCollider2D standingHitbox;
    [SerializeField] private CapsuleCollider2D crouchingHitbox;
    private bool canStand = true;
    [SerializeField] private BoxCollider2D triggerHitbox;


    void Start()
    {
        body = GetComponent<Rigidbody2D>(); // Setting the RigidBody2D component.
        //sr = GetComponent<SpriteRenderer>(); // Setting the SpriteRenderer component.
        animator = GetComponent<Animator>(); // Setting the Animator component. [OPTIONAL]

        jumpCoolDown = jumpCoolDownBase;    //een "Base" van iets is de standaard waarde; zodat deze later weer opgehaald kan worden
        runSpeed = runSpeedBase;
    }

    // Update() is called every frame.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded && canJump) 
            {
                canJump = false;
                Jump(); 
            }  // Checking on "Space" key pressed.
        }
        /*        if (Input.GetKey(KeyCode.A)) APressed = true; // Checking on "A" key pressed.
                if (Input.GetKey(KeyCode.D)) DPressed = true; // Checking on "D" key pressed.
                if (Input.GetKey(KeyCode.E)) EPressed = true; // Checking on "D" key pressed.*/

        movementX = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (chrouchModeOn == false && canLand == false)
            {
                ChrouchingHitboxEnable();
                runSpeed = runSpeedBaseChrouched;       //verandert de loopsnelheid
                spriteRenderer.sprite = chrouchingSprite;
                chrouchModeOn = true;
            }

         //   if (canLand == false) { spriteRenderer.sprite = chrouchingSprite; }     //verandert alleen naar de juiste sprite als je niet aan het vallen bent
        }


        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            if (canStand == true)
            {
                StandingUp();
            }

        }

        if ((Input.GetKey(KeyCode.LeftControl) == false) && canStand && chrouchModeOn && isGrounded)      //als je wilt opstaan en het mag weer
        {
            StandingUp();
            print("Sta weer");
        }


        if (canJump == false && jumpCoolDown >= 0)
        {
            jumpCoolDown -= Time.deltaTime;
            Debug.Log(canJump);
        }
        else if (canJump == false)
        {
            canJump = true;
            jumpCoolDown = jumpCoolDownBase;
            print("Can jump again");
        }

        if (canLand && isGrounded && canJump)       //gebeurd zodra je geland bent (misschien "canJump" weg)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                spriteRenderer.sprite = chrouchingSprite;
                ChrouchingHitboxEnable();
            }
            else
            {
                spriteRenderer.sprite = standingSprite;
            }

            canLand = false;
        }
    }




    // Update using for physics calculations.
    void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.transform.position, groundCheckRadius, groundLayer); // Checking if character is on the ground.

        if (movementX != 0) //zodat er niet keer 0 wordt gedaan
        {
            runSpeedDirection = movementX * runSpeed;
            body.velocity = new Vector2(runSpeedDirection, body.velocity.y);
        }
        else body.velocity = new Vector2(0, body.velocity.y);


        if (movementX == 1)     //tijdelijk om te draaien; gaat later via animation waarschijnlijk
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z); // Rotating the character object to the right.
        }
        else if (movementX == -1)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z); // Rotating the character object to the left.
        }
/*        // Left/Right movement.
        if (APressed)
        {
            body.velocity = new Vector2(-runSpeed, body.velocity.y); // Move left physics.
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z); // Rotating the character object to the left.
            APressed = false; // Returning initial value.
        }
        else if (DPressed)
        {
            body.velocity = new Vector2(runSpeed, body.velocity.y); // Move right physics.
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z); // Rotating the character object to the right.
            DPressed = false; // Returning initial value.
        }*/


        /* Setting jump sprite. [OPTIONAL]
        if (!isGrounded)
        {
            animator.enabled = false; // Turning off animation.
            sr.sprite = jumpSprite; // Setting the sprite.
        }
        else animator.enabled = true; // Turning on animation.
        */
    }



    private void OnTriggerStay2D(Collider2D collision)      //elke frame controleert die of er iets in het trigger veld zit
    {
        canStand = false;
    }

    private void OnTriggerExit2D(Collider2D collision)      //één keer
    {
        canStand = true;
    }

    // For the button on the screen to jump.
    private void Jump()     //zit in de knop om te springen; hoeft maar één keer uitgevoert te worden ("velocity" hoeft maar één keer gebruikt te worden)
    {      // Jumps.
        if (chrouchModeOn)
        {
            StandingHitboxEnable();
        }
        print("Jumping");
        body.velocity = new Vector2(0, jumpForce); // Jump physics.
        spriteRenderer.sprite = jumpSprite; // Setting the sprite.      Later niet voor bij springen maar als je valt de sprite veranderen (misschien in de animatie tho)
        canLand = true;
    }


    private void StandingUp()
    {
        StandingHitboxEnable();
        runSpeed = runSpeedBase;                //verandert de loopsnelheid
        chrouchModeOn = false;
        if (canLand == false) { spriteRenderer.sprite = standingSprite; }       //verandert alleen naar de juiste sprite als je niet aan het vallen bent
    }

    private void ChrouchingHitboxEnable()
    {
        crouchingHitbox.enabled = true;
        standingHitbox.enabled = false;
        triggerHitbox.enabled = true;       //trigger hitbox om te checken of je kan staan
    }

    private void StandingHitboxEnable()
    {
        crouchingHitbox.enabled = false;
        standingHitbox.enabled = true;
        triggerHitbox.enabled = false;
    }

}