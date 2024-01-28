using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BGenerate : MonoBehaviour
{
    [SerializeField] private float despawnTimer = 5;

    private void Start()
    {
        Destroy(gameObject, despawnTimer);
    }
}
