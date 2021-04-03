using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject[] players, extraCamera;
    public bool facingRight = true; //Depends on if your animation is by default facing right or left
    Animator animator;
    public bool Walk;

    public float speed;
    public float jumpForce;
    bool isJumping;
    Rigidbody2D rigidbody2d;


    //things preset for when the game starts.
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("MainCamera"));
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Walk = false;
    }

    private void FixedUpdate()
    {

        float move = Input.GetAxis("Horizontal");

        //look up checking if key is down
        //determining walk animation and setting walk to true or false.
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Walk", true);
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("Walk", false);
        }

        // determining which way character is moving and flipping to face that way.
        if (move > 0 && !facingRight)
        {
            Flip();
        }
            
        else if (move < 0 && facingRight)
        {
            Flip();
        }

        // if there is no horizontal movement STOP FUCKING WALKING.
        if (move == 0)
        {
            animator.SetBool("Walk", false);
        }

        rigidbody2d.velocity = new Vector2(speed * move, rigidbody2d.velocity.y);




        Jump();
    }
    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && !isJumping)
        {
            animator.SetBool("isJumping", true);

            isJumping = true;
            Walk = false;

            rigidbody2d.AddForce(new Vector2(rigidbody2d.velocity.x, jumpForce));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isJumping", false);

            isJumping = false;
            rigidbody2d.velocity = Vector2.zero;
        }
    }

    // flips the character to the direction they are moving. 
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    // makes sure only ONE player loads on each level
    private void OnLevelWasLoaded(int level)
    {
        FindStartPos();

        players = GameObject.FindGameObjectsWithTag("Player");
        extraCamera= GameObject.FindGameObjectsWithTag("MainCamera");

        if (players.Length > 1)
        {
            Destroy(players[1]);
            Destroy(extraCamera[1]);
        }
    }

    //find start position and put player there.
    void FindStartPos()
    {
        transform.position = GameObject.FindWithTag("StartPos").transform.position;
    }
}
