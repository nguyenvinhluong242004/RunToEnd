using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSpike : MonoBehaviour
{
    Animator anm;
    GameObject player;
    Rigidbody2D rb;
    bool isOn;
    Vector2 _po;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anm = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isOn = false;
        _po = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            if (player.transform.position.x - 7f < _po.x && player.transform.position.x + 7f > _po.x)
            {
                anm.SetBool("isOn", true);
                isOn = true;
            }
            else
            {
                isOn = false;
                anm.SetBool("isOn", false);
            }
            if (isOn)
            {
                if (player.transform.position.x < transform.position.x)
                    rb.velocity = new Vector2(-2f, 0);
                else
                    rb.velocity = new Vector2(2f, 0);
                if (transform.position.x > _po.x + 5f)
                    transform.position = new Vector3(_po.x + 5f, _po.y, transform.position.z);
                else if (transform.position.x < _po.x - 5f)
                    transform.position = new Vector3(_po.x - 5f, _po.y, transform.position.z);
            }
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        
    }
}
