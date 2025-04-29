using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 moveDirection;
    public float speed;
    public float jumpSpeed;
    public Transform myFeet;
    public LayerMask groundLayer;
    bool isGrounded;
    bool isFacingRight = true;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(myFeet.position, 0.1f, groundLayer);
        FlipSprite();
        Walk();
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            anim.SetTrigger("jump");
            rb.velocity = Vector2.up * jumpSpeed;
        }
    }

    void OnMove(InputValue value)
    {
        if(value != null)
        {
            moveDirection = value.Get<Vector2>();
        }
    }


    void Walk()
    {
        Vector2 playerVelocity = new Vector2(moveDirection.x,0) * speed;
        rb.velocity = playerVelocity;

        if(Mathf.Abs(playerVelocity.x) >0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning",false);
        }
    }
    

    void FlipSprite()
    {
        if(moveDirection.x<0 && isFacingRight)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x,180f,transform.rotation.z);
            isFacingRight = false;
        }
        else if(moveDirection.x>0 && !isFacingRight)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
            isFacingRight = true;
        }
    }

}
