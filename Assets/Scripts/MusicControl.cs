using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MusicControl : MonoBehaviour
{
    public GameObject on, off;
    public bool isMusic;
    GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        if (gameController.isMusic)
        {
            isMusic = true;
            off.SetActive(false);
        }    
        else
        {
            isMusic = false;
            on.SetActive(false);
        }
    }
    public void setMusic()
    {
        
        if (isMusic)
        {
            gameController.setMussic(false);
            Debug.Log("on");
            isMusic = false;
            off.SetActive(true);
            on.SetActive(false);
        }
        else
        {
            gameController.setMussic(true);
            Debug.Log("off");
            isMusic = true;
            on.SetActive(true);
            off.SetActive(false);
        } 
            
    }  
    public void reStart()
    {
        if (isMusic)
            off.SetActive(false);
        else
            on.SetActive(false);
    }    
}
