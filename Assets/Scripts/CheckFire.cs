using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFire : MonoBehaviour
{
    Animator anm;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anm = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            if (player.transform.position.x - 5f < transform.position.x && player.transform.position.x + 5f > transform.position.x)
            {
                anm.SetBool("isOn", true);
            }
            else
            {
                anm.SetBool("isOn", false);
            }
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
