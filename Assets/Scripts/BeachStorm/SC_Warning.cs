using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Warning : MonoBehaviour
{
    //[SerializeField] protected AudioSource audioSource;
    protected float despawnTimerBase = 2f;
    protected float despawnTimer;

    protected bool spriteRenderActive = false;
    [SerializeField] protected SpriteRenderer warningSpriteRenderer;

    protected virtual void Start()
    {
        //warningSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        despawnTimer = despawnTimerBase;
        //this.warningSpriteRenderer.enabled = false; //TEST
    }

    protected virtual void Update()
    {

        if (spriteRenderActive)
        {
            this.warningSpriteRenderer.enabled = true;
            spriteRenderActive = false;
        }

        if (this.warningSpriteRenderer.enabled == true && despawnTimer >= 0)
        {
            despawnTimer -= Time.deltaTime;
        }
        else if (this.warningSpriteRenderer.enabled == true)
        {
            this.warningSpriteRenderer.enabled = false;
            despawnTimer = despawnTimerBase;
            /*            audioSource.volume = Random.Range(0.8f, 1);
                        audioSource.pitch = Random.Range(0.8f, 1.2f);
                        audioSource.PlayOneShot(audioSource.clip);*/
        }
    }

}

