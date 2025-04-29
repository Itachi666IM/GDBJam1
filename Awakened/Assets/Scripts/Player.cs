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
    public LayerMask groundLayer;
    public Transform myFeet;
    public Transform jumpTarget;
    bool isGrounded;
    bool isFacingRight = true;
    bool once;
    [HideInInspector]public bool isDead;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!isDead)
        {

            isGrounded = Physics2D.OverlapCircle(myFeet.position, 0.1f, groundLayer);
            FlipSprite();
            Walk();
        }
        else
        {
            if(!once)
            {
                once = true;
                anim.SetTrigger("dead");
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
            }
            
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

    void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }
    
    void OnJump(InputValue value)
    {
        if(value.isPressed && isGrounded)
        {
            Debug.Log("jumping");
            anim.SetTrigger("jump");
            transform.position = Vector2.MoveTowards(transform.position, jumpTarget.position,1.6f);
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
