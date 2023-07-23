using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    Animator anm;
    public GameObject gift;
    AudioSource aus;
    public AudioClip sound;
    GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        anm = GetComponent<Animator>();
        aus = FindObjectOfType<AudioSource>();
        gameController = FindObjectOfType<GameController>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        Vector2 point = contact.point;
        // Nhận biết va chạm mặt nào của player
        Vector2 normal = contact.normal;
        {
            if (normal == Vector2.down)
            {
                if (gameController.isMusic)
                    aus.PlayOneShot(sound);
                anm.SetBool("isBreak", true);
                if (gift)
                {
                    Debug.Log("Creat");
                    Instantiate(gift, transform.position + new Vector3(2, 0, 0), transform.rotation);
                }    
                Invoke("getDestroy", 0.05f);
            }
        }
    }
    void getDestroy()
    {
        Destroy(gameObject);
    }
}
