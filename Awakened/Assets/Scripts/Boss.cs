using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Vector2 defaultPos;
    Transform target;
    public Transform[] targets;
    public GameObject firePrefab;
    public Transform[] spawnpoints;
    public Transform[] defaultPoints;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        defaultPos = defaultPoints[Random.Range(0, defaultPoints.Length)].position;
        target = targets[Random.Range(0, targets.Length)];

    }
    public void GoToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, 1f);
    }

    public void ResetPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, defaultPos, 1f);
        target = targets[Random.Range(0,targets.Length)];
    }

    public void Attack()
    {
        Transform randomSpawnPoint = spawnpoints[Random.Range(0, spawnpoints.Length)];
        Instantiate(firePrefab, randomSpawnPoint.position,randomSpawnPoint.rotation);
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position,target.position)<0.5f)
        {
            anim.SetTrigger("attack");
        }
    }
}
