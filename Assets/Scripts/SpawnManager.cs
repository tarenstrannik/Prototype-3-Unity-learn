using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] obstaclePrefab;
    public Vector3 spawnPos = new Vector3(30,0,0);

    private float startDelay = 1f;
    private float repeatRateMin = 2f;
    private float repeatRateMax = 4f;

    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        Invoke("SpawnObstacle",startDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnObstacle()
    {
        int obstacleIndex = Random.Range(0, obstaclePrefab.Length);
        if (!playerControllerScript.isGameOver)
        {
            Instantiate(obstaclePrefab[obstacleIndex], spawnPos, obstaclePrefab[obstacleIndex].transform.rotation);
        }
        float repeatDelay = Random.Range(repeatRateMin, repeatRateMax);
        Invoke("SpawnObstacle", repeatDelay);
    }
}
