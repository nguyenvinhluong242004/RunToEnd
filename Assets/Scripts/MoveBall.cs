using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    float speed = -1f;
    // Update is called once per frame
    void Update()
    {
        if (transform.eulerAngles.z > 200f || transform.eulerAngles.z < 100f)
            speed = -speed;
        transform.Rotate(0, 0, 35 * speed * Time.deltaTime);
    }
}
