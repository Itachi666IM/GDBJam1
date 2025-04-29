using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trident : MonoBehaviour
{
    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.isDead = true;
            Invoke(nameof(ReloadScene), 2f);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

