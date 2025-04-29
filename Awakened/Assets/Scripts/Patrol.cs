using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : Enemy
{
    public Transform detector;
    public LayerMask groundLayer;
    public float speed;
    public int inputX = -1;

    public bool isPatrolling;
    Animator anim;
    bool isWaiting;
    public float waitTime;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!isPatrolling)
        {
            return;
        }
        if (isWaiting)
        {
            return;
        }
        RaycastHit2D hit = Physics2D.Raycast(detector.position, Vector2.down, 1f, groundLayer);
        if (hit.point == Vector2.zero)
        {
            StartCoroutine(WaitThenTurn());
        }
        else
        {
            if (isWaiting == false)
            {
                transform.position += Vector3.right * inputX * speed * Time.deltaTime;
            }
        }
    }

    IEnumerator WaitThenTurn()
    {

        inputX = -inputX;


        isWaiting = true;
        anim.SetBool("isWaiting", true);

        yield return new WaitForSeconds(waitTime);

        if (inputX == -1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (inputX == 1)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }


        isWaiting = false;
        anim.SetBool("isWaiting", false);
    }
}
