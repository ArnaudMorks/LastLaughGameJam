using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closset : MonoBehaviour
{
    public GameObject playerSprites;
    public Collider2D playerCollider;
    public Transform playerLocation;
    public CharackterController2D controller2D;
    public Transform tf;
    public SpriteRenderer openDoorsSprite;
    private bool inCloset = false;
    /* Start is called before the first frame update
    void Start()
    {
        
    }
    */
    // Update is called once per frame
    void Update()
    {
        if (controller2D.nearClosset == true && controller2D.EPressed == true)
        {
            openDoorsSprite.enabled = false;
            Debug.Log("wein baby");
            playerSprites.SetActive(false);
            playerCollider.enabled = false;
            inCloset = true;
        }
        if (controller2D.DPressed == true || controller2D.APressed == true)
        {
            if (inCloset == true)
            {
                openDoorsSprite.enabled = true;
                playerSprites.SetActive(true);
                playerCollider.enabled = true;
                playerLocation.position = new Vector3(tf.position.x, (tf.position.y + 0.5f), tf.position.z);
                inCloset = false;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if (collisionInfo.CompareTag("Player"))
        {
            
            //Debug.Log(collisionInfo);
            //setLockerLocation.locationClosset = tf.position.x;
            controller2D.locationClosset = 3.6f;
            controller2D.nearClosset = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        controller2D.nearClosset = false;
        Debug.Log(controller2D.locationClosset);
    }
}
