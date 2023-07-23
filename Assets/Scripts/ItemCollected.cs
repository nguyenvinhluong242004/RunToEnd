using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollected : MonoBehaviour
{
    int cheeriCollect;
    GameObject[] cheri;
    int count;
    public TextMeshProUGUI scoreText;
    AudioSource aus;
    public AudioClip sound;
    GameController gameController;
    void Start()
    {
        cheeriCollect = 0;
        aus = FindObjectOfType<AudioSource>();
        gameController = FindObjectOfType<GameController>();
        GameObject scoreTextObject = GameObject.Find("Cheeri Text");
        if (scoreTextObject != null)
        {
            scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
        }
        cheri = GameObject.FindGameObjectsWithTag("Cheeri");
        count = cheri.Length;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cheeri"))
        {
            if (gameController.isMusic)
                aus.PlayOneShot(sound);
            Destroy(collision.gameObject);
            cheeriCollect++;
            if (scoreText)
                scoreText.text = ": " + cheeriCollect;
        }
    }
    public int getStar()
    {
        if (cheeriCollect == count)
            return 3;
        if (cheeriCollect >= 2 * count / 3)
            return 2;
        return 1;
    }
}
