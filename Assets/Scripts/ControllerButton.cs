using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerButton : MonoBehaviour
{
    public GameController gameController;
    public GameObject cam;
    public int key;
    AudioSource aus;
    public AudioClip sound;
    public MusicControl muc;
    // Start is called before the first frame update
    void Start()
    {
        aus = FindObjectOfType<AudioSource>();
        gameController = FindObjectOfType<GameController>();
        muc = FindObjectOfType<MusicControl>();
        cam = GameObject.Find("Main Camera");
    }
    // 0: Player1   1: Player2   2: Player3:   3: Player4   4: PlayGame   5: Setting   6: Back   7: BackGame
    // 8: MusicOn   9: MusicOff  10: next    11: replace
    void OnMouseDown()
    {
        Debug.Log(key);
        if(gameController.isMusic)
            aus.PlayOneShot(sound);
        if (0 <= key && key <= 3)
        {
            gameController.setIdPlayer(key);
        }
        else if (key == 4)
            cam.transform.position = new Vector3(cam.transform.position.x, 10f, cam.transform.position.z);
        else if (key == 5)
            cam.transform.position = new Vector3(20f, cam.transform.position.y, cam.transform.position.z);
        else if (key == 6)
            cam.transform.position = new Vector3(0f, 0f, cam.transform.position.z);
        else if (key == 7)
        {
            gameController.setLevel();
            gameController.getStart(true);
        }    
        else if (key == 8)
            muc.setMusic();
        else if (key == 9)
            muc.setMusic();
        else if (key==10)
        {
            CompleteLevel();
        }   
        else if (key==11)
        {
            ReStartLevel();
        }    
        else if (key<0)
        {
            gameController.levelPlay = -(key + 1);
            Debug.Log(gameController.levelPlay);
            startGame();
        }  
        
    }
    void startGame()
    {
        Debug.Log("start game");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - key);
        gameController.getStart(false);
        gameController.getLevel();
    }
    void CompleteLevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        gameController.setLevel();
        gameController.levelUp();
    }
    void ReStartLevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameController.restartLevel();
    }    
}
