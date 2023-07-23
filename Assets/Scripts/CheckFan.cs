using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFan : MonoBehaviour
{
    Animator anm;
    GameObject player;
    public AudioSource aus;
    public AudioClip sound;
    bool isAudio;
    GameController gameController;
    void Start()
    {
        gameController=FindObjectOfType<GameController>();
        player = GameObject.FindGameObjectWithTag("Player");
        anm = GetComponent<Animator>();
        aus = FindObjectOfType<AudioSource>();
        isAudio = false;
    }
    void Update()
    {
        if (player)
        {
            if (player.transform.position.x - 4f < transform.position.x && player.transform.position.x + 4f > transform.position.x)
            {
                if (!isAudio)
                {
                    if (gameController.isMusic)
                        aus.Play();
                    isAudio = true;
                }
                anm.SetBool("isOn", true);
            }
            else
            {
                if (isAudio)
                {
                    if (gameController.isMusic)
                        aus.Stop();
                    isAudio = false;
                }
                anm.SetBool("isOn", false);
            }
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
       
    }
}
