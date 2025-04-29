using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float jumpSpeed;
    public LayerMask groundLayer;

    BoxCollider2D myFeetCollider;
    bool isGrounded;
    bool isFacingRight = true;
    bool once;
    [HideInInspector]public bool isDead;
    Animator anim;

    float inputX;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        myFeetCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (!isDead)
        {
            inputX = Input.GetAxisRaw("Horizontal");
            
            isGrounded = myFeetCollider.IsTouchingLayers(groundLayer);

            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
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

   
    void Jump()
    {
        Debug.Log("jumping");
        anim.SetTrigger("jump");
        rb.velocity = Vector2.up * jumpSpeed;
    }

    void Walk()
    {
        Vector2 playerVelocity = new Vector2(inputX,0) * speed;
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
        if(inputX<0 && isFacingRight)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x,180f,transform.rotation.z);
            isFacingRight = false;
        }
        else if(inputX>0 && !isFacingRight)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
            isFacingRight = true;
        }
    }

}
