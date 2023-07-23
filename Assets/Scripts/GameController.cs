using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameController : MonoBehaviour
{
    public int levelPlay;
    public GameObject[] player;
    public GameObject[] levels;
    public GameObject[] levelsPlay;
    public GameObject _start, _play, _end, levelPresent, playerPresent, _back;
    public GameObject complete;
    public GameObject[] starCom;
    public GameObject point;
    GameObject bgrPlayer;
    public int id;
    //public GameObject point; // đểm khởi đầu của object khi khởi tạo
    public bool isMusic;
    AudioSource aus;
    public ItemCollected item;
    CameraController cam;

    // Start is called before the first frame update
    void Start()
    {
        _end.SetActive(false);
        _back.SetActive(false);
        isMusic = true;
        aus = GetComponent<AudioSource>();
        cam = FindObjectOfType<CameraController>();
        Debug.Log(levelPlay);
        levels = GameObject.FindGameObjectsWithTag("Level");
        bgrPlayer = GameObject.Find("SetPlayer"); 
        complete = GameObject.FindGameObjectWithTag("Complete");
        if (complete)
        {
            starCom = GameObject.FindGameObjectsWithTag("StarCom");
            foreach (GameObject sta in starCom)
                sta.SetActive(false);
            complete.SetActive(false);
        }
    }
    public void setIdPlayer(int idd)
    {
        id = idd;
        bgrPlayer.transform.position = new Vector3(16.34f + 2.5f * idd, bgrPlayer.transform.position.y, bgrPlayer.transform.position.z);
        Debug.Log(id);
        Debug.Log("ok");
    }
    public void setMussic(bool sta)
    {
        isMusic = sta;
        if (isMusic)
            aus.Play();
        else
            aus.Stop();
    }    
    public void getComplete()
    {
        complete.SetActive(true);
        for (int i = 0; i < item.getStar(); i++)
            starCom[i].SetActive(true);
    }
    public void getStart(bool sta)
    {
        if (sta)
        {
            _back.SetActive(false);
            cam.setSart();
        }
        _start.SetActive(sta);
    }
    public void getEnd(bool sta)
    {
        _end.SetActive(sta);
    }
    public void setLevel()
    {
        Destroy(levelPresent);
    }
    public void restartLevel()
    {
        Destroy(levelPresent);
        getLevel();
    }
    public void getLevel()
    {
        if (levelPlay<levelsPlay.Length)
        {
            complete.SetActive(false);
            if (levelPresent)
                Destroy(levelPresent);
            
            levelPresent = Instantiate(levelsPlay[levelPlay], transform.position, transform.rotation);
            point = GameObject.FindGameObjectWithTag("Point");
            if (point)
                Destroy(point);
            while (!point)
                point = GameObject.FindGameObjectWithTag("Point");
            if (point)
            {
                Debug.Log("Load");
                if (playerPresent)
                    Destroy(playerPresent);
                playerPresent = Instantiate(player[id], point.transform.position, transform.rotation);
                playerPresent.transform.SetParent(levelPresent.transform);
            }
            item = FindObjectOfType<ItemCollected>();
            _back.SetActive(true);
            cam.setIsEnd();
        }
        else
            getStart(true); 
    }
    public void levelUp()
    {
        levelPlay++;
        if (levelPlay < levelsPlay.Length)
        {
            getLevel();
        }    
        else
        {
            _start.SetActive(false);
            _end.SetActive(true);
        }    
    }    
}
