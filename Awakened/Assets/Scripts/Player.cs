using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 moveDirection;
    public float speed;
    public float jumpSpeed;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public Transform attackPoint;
    public float attackRadius;
    public int damageAmount;
    public Transform myFeet;
    public Transform jumpTarget;
    bool isGrounded;
    bool isFacingRight = true;
    bool once;
    [HideInInspector]public bool isDead;
    Animator anim;

    AudioSource mySource;
    public AudioSource sfx;
    public AudioClip jumpSound;
    public AudioClip swordSound;
    public AudioClip deathSound;

    public GameObject deathEffect;
    public GameObject slashEffect;

    [HideInInspector] public bool isBossDead; 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        mySource = GetComponent<AudioSource>();
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
            moveDirection = Vector2.zero;
            if(!once)
            {
                once = true;
                sfx.PlayOneShot(deathSound);
                Instantiate(deathEffect,transform.position,transform.rotation);
                anim.SetTrigger("dead");
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
                Invoke(nameof(ReloadScene), 2f);
            }
            
        }

        if(isBossDead)
        {
            Invoke(nameof(BossFightOver), 2f);
        }
        
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void BossFightOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Walk()
    {
        Vector2 playerVelocity = new Vector2(moveDirection.x,0) * speed;
        rb.velocity = playerVelocity;

        if(Mathf.Abs(playerVelocity.x) >0)
        {
            anim.SetBool("isRunning", true);
            mySource.enabled = true;
        }
        else
        {
            anim.SetBool("isRunning",false);
            mySource.enabled = false;
        }
    }

    void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }
    
    void OnJump(InputValue value)
    {
        if(value.isPressed && isGrounded &&!isDead)
        {
            anim.SetTrigger("jump");
            sfx.PlayOneShot(jumpSound);
            transform.position = Vector2.MoveTowards(transform.position, jumpTarget.position,1.6f);
        }
    }

    void OnFire(InputValue value)
    {
        if(value.isPressed && !isDead)
        {
            anim.SetTrigger("attack");
            sfx.PlayOneShot(swordSound);
            
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

    public void DetectEnemyAndDealDamage()
    {
        Instantiate(slashEffect, attackPoint.position, attackPoint.rotation);
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, enemyLayer);
        if(hit != null)
        {
            hit.GetComponent<Enemy>().TakeDamage(damageAmount);
        }
    }

}
