using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BulletDodge : MonoBehaviour
{
    [SerializeField] private AudioClip[] shotClips;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float audioTimer = 2;
    private bool soundPlayed = false;

    [SerializeField] private float despawnTimer = 5;
    [SerializeField] private float speed;

    [SerializeField] private int isUp;
    [SerializeField] private float upY;
    [SerializeField] private float downY;
    [SerializeField] private SC_WarningUp warningUp;
    [SerializeField] private SC_WarningDown warningDown;
    /*    [SerializeField] private SpriteRenderer warningUpSprite;
        [SerializeField] private SpriteRenderer warningDownSprite;*/
    private SC_GamePause gamePause;

    private void Start()
    {
        gamePause = FindObjectOfType<SC_GamePause>();
        warningUp = FindObjectOfType<SC_WarningUp>();
        warningDown = FindObjectOfType<SC_WarningDown>();

/*        warningUpSprite = warningUp.GetComponent<SpriteRenderer>();
        warningDownSprite = warningUp.GetComponent<SpriteRenderer>();*/

        isUp = Random.Range(0, 2);
        audioSource.clip = shotClips[isUp];

        if (isUp == 0)
        {
            warningUp.SpriteRenderActiveUp = true;
            transform.position = new Vector3(transform.position.x, upY, 0);
        }
        else 
        {
            warningDown.SpriteRenderActiveDown = true;
            transform.position = new Vector3(transform.position.x, downY, 0);
        }

        print(isUp);
        Destroy(gameObject, despawnTimer);
    }

    private void Update()
    {

        if (audioTimer >= 0)
        {
            audioTimer -= Time.deltaTime;
        }
        else if (soundPlayed == false)
        {
            audioSource.volume = Random.Range(0.8f, 1);
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(audioSource.clip);
            soundPlayed = true;
        }
    }

    private void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(speed * Time.fixedDeltaTime, 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.timeScale == 0)
        {
            return;
        }


        SC_CharacterController2D player = collision.gameObject.GetComponent<SC_CharacterController2D>();

        if (player != null && collision.isTrigger == false)
        {
            OnHit();
        }
    }

    private void OnHit()
    {
        gamePause.playerDead = true;
    }

}
