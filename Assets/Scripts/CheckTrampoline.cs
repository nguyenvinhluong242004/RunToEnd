using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTrampoline : MonoBehaviour
{
    Animator anm;
    public AudioSource aus;
    public AudioClip sound;
    GameController gameController;
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        anm = GetComponent<Animator>();
        aus = FindObjectOfType<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (gameController.isMusic)
                aus.PlayOneShot(sound);
            anm.SetBool("isOn", true);
            Invoke("setTrampoline", 1f);
        }
    }
    void setTrampoline()
    {
        anm.SetBool("isOn", false);
    }   
}
