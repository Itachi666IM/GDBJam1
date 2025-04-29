using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Note : MonoBehaviour
{
    Player player;
    public GameObject fadePanel;
    private void Start()
    {
        player = FindObjectOfType<Player>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            fadePanel.SetActive(true);
            player.gameObject.SetActive(false);
        }
    }
}
