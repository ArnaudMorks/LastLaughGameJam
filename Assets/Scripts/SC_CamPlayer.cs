using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CamPlayer : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float offsetX;
    [SerializeField] float offsetY;
    //  [SerializeField] Vector3 offsets;

    private void Start()
    {
        playerTransform = FindObjectOfType<SC_CharacterController2D>().transform;
    }

    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x + offsetX, playerTransform.position.y + offsetY, -10);
    }
}