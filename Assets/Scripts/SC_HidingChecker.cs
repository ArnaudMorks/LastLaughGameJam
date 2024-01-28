using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_HidingChecker : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D standingHitbox;
    [SerializeField] private CapsuleCollider2D crouchingHitbox;
    public bool imHidden = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ChrouchingHitboxEnable();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            ChrouchingHitboxDisable();
        }
    }
    private void ChrouchingHitboxEnable()
    {
        standingHitbox.offset = new Vector2(-0.02f, -0.07f);
    }
    private void ChrouchingHitboxDisable()
    {
        standingHitbox.offset = new Vector2(-0.02f, 0.1f);
    }
    private void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if (collisionInfo.CompareTag("Box"))
        {
            imHidden = true;
            Debug.Log("in box");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            imHidden = false;
            Debug.Log("out box");
        }
    }
}
