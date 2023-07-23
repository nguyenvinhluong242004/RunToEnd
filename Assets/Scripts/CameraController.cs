using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    bool isEnd;
    void Start()
    {
        isEnd = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if (!isEnd)
        {
            if (player)
            {
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
            }
            else
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
        }    
    }
    public void setSart()
    {
        transform.position = new Vector3(0, 10, transform.position.z);
        isEnd = true;
    }    
    public void setIsEnd()
    {
        isEnd = false;
    }    
}
