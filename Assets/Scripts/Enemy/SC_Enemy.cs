using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle = 0,
    walking,
    shooting
}
public class Enemy : MonoBehaviour
{
    private EnemyState enemyState;
    private Animator animator;
    [SerializeField] private GameObject myGunLight;
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
    [SerializeField] private bool affectedByJukeBox = false;
    [SerializeField] private GameObject nearJukeBox;
    [SerializeField] private AudioSource nearJukeBoxAudio;
    [SerializeField] private SC_Jukebox sc_juke;
    private bool jukeboxdelay = false;
    private bool dancingQueen;
    public bool jukeboxPlaying = false;
    /* Random Idle Explain
    1 = Always   
    2 = 50% every 2 sec 2 sec idle
    3 = 33% every 2 sec 2 sec idle              |
    4 = 25% every 2 sec 2 sec idle              |
    Enz enz                                    \ /
    */
    [SerializeField] private int randomChance = 1; 
    private bool allowRandom = true;
    private float randomChecker;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // Calculate the left- and right-side of the enemy
        leftSide = this.transform.position.x - leftOfMe;
        rightSide = this.transform.position.x + rightOfMe;
        playerLocation = FindObjectOfType<SC_CharacterController2D>().transform;
    }

    void Update()
    {
        if (body.velocity.x == 0)
        {
            enemyState = EnemyState.idle;
        }
        else if (body.velocity.x != 0)
        {
            enemyState = EnemyState.walking;
        }


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

        if (affectedByJukeBox)
        {
            JukeboxStuff();
        }

        if (allowRandom)
        {
            allowRandom = false;
            Invoke("RandomStandstill", 2f);
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
    void RandomStandstill()
    {
        randomChecker = Random.Range(0, randomChance);
        if (randomChecker == 0)
        {
            patrol = false;
            allowRandom = true;
        }
        else if(randomChecker != 0)
        {
            patrol = true;
            allowRandom = true;
        }
    }
    void JukeboxStuff()
    {
        jukeboxPlaying = sc_juke.JukeBoxOn;
        dancingQueen = true;
        if (jukeboxPlaying && dancingQueen)
        {
            runSpeed = 2f;
            if (jukeboxdelay == false)
            {
                Invoke("jukeboxDelay", 2f);
            }
            else if (myCurrentXLocation <= (nearJukeBox.transform.position.x + 0.2f) && myCurrentXLocation >= (nearJukeBox.transform.position.x - 0.2f) && jukeboxdelay)
            {
                goingLeft = false;
                goingRight = false;
                dancingQueen = false;
                patrol = false;

                Invoke("TurnItOff", 5f);
                Invoke("ResetJukeBox", 5.1f);
            }
            else if (myCurrentXLocation > nearJukeBox.transform.position.x - 0.3f && jukeboxdelay)
            {
                goingLeft = true;
            }
            else if (myCurrentXLocation < nearJukeBox.transform.position.x + 0.3 && jukeboxdelay)
            {
                goingRight = true;
            }
        }
    }
    void jukeboxDelay()
    {
        jukeboxdelay = true;
    }
    void TurnItOff()
    {
        nearJukeBoxAudio.enabled = false;
        sc_juke.JukeBoxOn = false;
        patrol = true;
        runSpeed = 0.4f;
        jukeboxPlaying = false;
    }
    void ResetJukeBox()
    {
        nearJukeBoxAudio.enabled = true;
        jukeboxdelay = false;
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

        
        animator.SetInteger("EnemyState", (int)enemyState);
    }
    // Detects if the player enters the trigger.
    private void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if (collisionInfo.CompareTag("Player"))
        {
            playerSpotted = true;
            myGunLight.SetActive(true);
            runSpeed = 2.6f;
            enemyState = EnemyState.shooting;
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
