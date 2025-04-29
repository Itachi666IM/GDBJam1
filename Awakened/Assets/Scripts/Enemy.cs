using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int health;
    public bool isBoss;
    public Slider healthSlider;
    public GameObject deathEffect;

    public AudioSource myAudio;
    public AudioClip death;

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (isBoss)
        {
            healthSlider.value = health;
        }
        if (health <= 0)
        {
            myAudio.PlayOneShot(death);
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().isDead = true;
        }
    }

}
