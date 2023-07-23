using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    Animator anm;
    GameObject player;
    Rigidbody2D rb;
    [SerializeField]
    LayerMask jumpAbleGround;
    BoxCollider2D coll;
    enum MovemenState { idle, hit, fall};
    MovemenState state;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        anm = GetComponent<Animator>();
        state = MovemenState.idle;
        anm.SetInteger("state", (int)state);
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            if (rb.gravityScale == 1 && isGrounded())
            {
                state = MovemenState.fall;
                anm.SetInteger("state", (int)state);
                Invoke("onDestroy", 0.3f);
            }
            else if (player.transform.position.x - 0.5f < transform.position.x && player.transform.position.x + 0.5f > transform.position.x)
            {
                rb.gravityScale = 1;
            }    
            else if (player.transform.position.x - 5f < transform.position.x && player.transform.position.x + 5f > transform.position.x)
            {
                state = MovemenState.hit;
                anm.SetInteger("state", (int)state);
            }
            else
            {
                state = MovemenState.idle;
                anm.SetInteger("state", (int)state);
            }
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpAbleGround);
    }
    void onDestroy()
    {
        Destroy(gameObject);
    }
}
