using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 15f;

    private PlayerController playerControllerScript;
    private float leftBoundary = -10f;

    void Start()
    {
        playerControllerScript=GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!playerControllerScript.isGameOver&& playerControllerScript.gameStarted)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed* playerControllerScript.speedCoef);
        }
        if(transform.position.x< leftBoundary && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
