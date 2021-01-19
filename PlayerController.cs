using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    public bool facingRight = true; //Depends on if your animation is by default facing right or left
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    public bool Walk;
    //bool Jump; // TODO need to add jump physics




    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>(); // gets area so sprite can move
        animator = GetComponent<Animator>();
        Walk = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, 0);
    }


    //basically update but runs faster? 
    void FixedUpdate()
    {
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
        if (horizontal > 0 && !facingRight)
            Flip();
        else if (horizontal < 0 && facingRight)
            Flip();

        // horizontal movement
        Vector2 position = rigidbody2d.position;
        position.x = position.x + 5.0f * horizontal * Time.deltaTime;
        rigidbody2d.MovePosition(position);
        
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
