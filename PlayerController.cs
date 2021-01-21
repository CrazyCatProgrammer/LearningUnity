using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool facingRight = true; //Depends on if your animation is by default facing right or left
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    public bool Walk;
    float horizontal;

    public float speed;
    public float jumpForce;
    bool isJumping;
    Rigidbody2D rigidbody2d;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Walk = false;
    }

    private void FixedUpdate()
    {

        float move = Input.GetAxis("Horizontal");

        //determining walk animation and setting walk to true or false.
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool("Walk", true);
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("Walk", false);
        }


        // determining which way character is moving and flipping to face that way.
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

        rigidbody2d.velocity = new Vector2(speed * move, rigidbody2d.velocity.y);

        Jump();
    }
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;

            rigidbody2d.AddForce(new Vector2(rigidbody2d.velocity.x, jumpForce));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
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
}
