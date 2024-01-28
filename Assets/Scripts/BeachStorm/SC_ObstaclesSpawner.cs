using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ObstaclesSpawner : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float offsetX;

    public GameObject currentObstacle;
    private float spawnRate = 2;
    private float spawnRateUp = 9;
    private float spawnTimer = 0;
    private float spawnTimerUp = 0;
    private float heightOffset = 4.5f;
    [SerializeField] private GameObject[] obstaclesArray;


    // Start is called before the first frame update
    void Start()
    {
        playerTransform = FindObjectOfType<SC_CharacterController2D>().transform;

        SpawnObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x + offsetX, transform.position.y, 0);

        if (spawnTimer < spawnRate)
        {
            spawnTimer += Time.deltaTime;        // Is hetzelfde als: "timer = timer + Time.deltaTime;" zo komt timer aan seconden (via deltaTime).
        }
        else
        {
            SpawnObstacle();
            spawnTimer = 0;
        }

        if (spawnTimerUp < spawnRateUp)
        {
            spawnTimerUp += Time.deltaTime;
        }
        else
        {
            spawnRate *= 0.9f;
            spawnTimerUp = 0;
        }

        if (spawnRate <= 0.33) { spawnRate = 2; }   //reset de spawn rate

    }

    void SpawnObstacle()
    {
        //float lowestPoint = transform.position.y - heightOffset;
        //float highestPoint = transform.position.y + heightOffset;

        currentObstacle = obstaclesArray[Random.Range(0, obstaclesArray.Length)];

        Instantiate(currentObstacle, new Vector3(transform.position.x, currentObstacle.transform.position.y, 0), transform.rotation);
    }
}
