using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollow : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    public int currentIdx = 0;
    public float speed = 2f;
    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(waypoints[currentIdx].transform.position,transform.position)<0.1f)
        {
            currentIdx++;
            if (currentIdx>=waypoints.Length)
            {
                currentIdx = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentIdx].transform.position, Time.deltaTime * speed);
    }
}
