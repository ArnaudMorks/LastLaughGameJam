using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BarbedWire : MonoBehaviour
{
    [SerializeField] float despawnTimer = 7;
    private int mirrored;

    private void Start()
    {
        Destroy(gameObject, despawnTimer);

        mirrored = Random.Range(0, 2);

        if (mirrored == 1)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
        }
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
        Time.timeScale = 0;
    }
}
