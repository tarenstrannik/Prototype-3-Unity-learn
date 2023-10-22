using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public ParticleSystem explosionParticles;
    public ParticleSystem dirtParticles;
    public float playerJumpForce = 10f;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    public float gravityModifier = 10;
    //private bool isOnGround = true;
    public bool isGameOver = false;
    private int jumpsNumber = 0;
    private int maxJumpsNumber = 2;
    public float walkSpeed = 7.5f;
    public float speedCoef = 1f;

    private float playerScore =0f;
    private int playerScoreInt = 0;

    public bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted)
        { 
            if(transform.position.x>=0f)
            {
                gameStarted = true;
                dirtParticles.Play();
            }
            else
            {
                playerAnim.SetFloat("Speed_f", 0.49f);
                transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
                dirtParticles.Stop();
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && jumpsNumber < maxJumpsNumber && !isGameOver)
            {
                jumpsNumber++;
                playerRb.AddForce(Vector3.up * playerJumpForce, ForceMode.Impulse);
                //isOnGround = false;
                playerAnim.SetTrigger("Jump_trig");
                dirtParticles.Stop();
                playerAudio.PlayOneShot(jumpSound, 1.0f);
            }
            if (Input.GetButton("SuperSpeed") && !isGameOver)
            {
                speedCoef = 2f;
                playerAnim.SetFloat("Speed_f", 2f);

            }
            else
            {
                playerAnim.SetFloat("Speed_f", 1f);
                speedCoef = 1f;
            }
            if (!isGameOver)
            {
                playerScore += Time.deltaTime * speedCoef;
                if (playerScoreInt != Mathf.FloorToInt(playerScore))
                {
                    playerScoreInt = Mathf.FloorToInt(playerScore);
                    Debug.Log("Player score: " + playerScoreInt);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !isGameOver)
        {
            //isOnGround = true;
            jumpsNumber = 0;
            dirtParticles.Play();
        }
        else if(collision.gameObject.CompareTag("Obstacle") && !isGameOver)
        {
            isGameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticles.Play();
            Debug.Log("Game Over. Your score: "+ playerScoreInt);
            dirtParticles.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
