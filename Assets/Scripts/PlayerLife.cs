using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    Animator anm;
    AudioSource aus;
    public AudioClip sound;
    GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        anm = GetComponent<Animator>();
        aus = FindObjectOfType<AudioSource>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            if (gameController.isMusic)
                aus.PlayOneShot(sound);
            Die();
        }    
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            if (gameController.isMusic)
                aus.PlayOneShot(sound);
            Die();
        }
    }
    void Die()
    {
        anm.SetTrigger("death");
    }
    void reStartLevel()
    {
        gameController.restartLevel();
    }
}
