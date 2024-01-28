using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Shoveling : MonoBehaviour
{
    private bool nearShovel = false;
    [SerializeField] private GameObject shovel1;
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject shovel2;
    [SerializeField] private AudioClip audioClose;
    [SerializeField] private AudioSource audioSource;
    private bool activateOnce = true;
    void Update()
    {
        if (nearShovel)
        {
            if (Input.GetKey(KeyCode.E) && activateOnce)
            {
                activateOnce = false;
                shovel1.SetActive(false);
                audioSource.clip = audioClose;
                audioSource.volume = Random.Range(0.8f, 1);
                audioSource.pitch = Random.Range(0.8f, 1.2f);
                audioSource.PlayOneShot(audioSource.clip);
                Invoke("Shoveling", 1.5f);
            }
        }
    }
    void Shoveling()
    {
        shovel2.SetActive(true);
        floor.SetActive(false);
    }





    private void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if (collisionInfo.CompareTag("Player"))
        {
            nearShovel = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            nearShovel = false;
        }
    }
}
