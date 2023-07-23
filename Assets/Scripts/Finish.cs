using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    AudioSource aus;
    public AudioClip sound;
    bool completeLevel;
    GameController gameController;
    void Start()
    {
        aus = FindObjectOfType<AudioSource>();
        gameController = FindObjectOfType<GameController>();
        completeLevel = false;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !completeLevel)
        {
            if (gameController.isMusic)
                aus.PlayOneShot(sound);
            completeLevel = true;
            gameController.getComplete();
        }    
    } 
}
