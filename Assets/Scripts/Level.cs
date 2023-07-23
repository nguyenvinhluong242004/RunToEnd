using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Level : MonoBehaviour
{
    public int id;
    public GameObject[] star;
    public GameObject _lock;
    GameController gameController;
    
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        star = new GameObject[3];
        Transform[] childObjects = transform.GetComponentsInChildren<Transform>(true); // tìm object con
        int i = 0;
        foreach (Transform child in childObjects)
        {   
            if (child.CompareTag("Star"))
            {
                star[i] = child.gameObject;
                i++;
            }
            else if (child.name == "lock")
            {
                _lock = child.gameObject;
            }
        }
    }
    // Update is called once per frame
    public void setStar(int k)
    {
        for (int i = 0; i < star.Length; i++)
            star[i].SetActive(false);
        for (int i = 0; i < k; i++)
            star[i].SetActive(true);
    }
    public void setLock(int k)
    {
        if (k == 1)
            _lock.SetActive(false);
    }    
}
