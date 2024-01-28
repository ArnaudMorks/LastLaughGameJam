using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Mine : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] float despawnTimer = 7;

    private void Start()
    {
        Destroy(gameObject, despawnTimer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.timeScale == 0)
        {
            return;
        }


        SC_CharacterController2D player = collision.gameObject.GetComponent<SC_CharacterController2D>();

        if (player != null)
        {
            OnExplode();
        }
    }

    private void OnExplode()
    {
        Time.timeScale = 0;
        audioSource.volume = Random.Range(0.8f, 1);
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(audioSource.clip);
    }

}
