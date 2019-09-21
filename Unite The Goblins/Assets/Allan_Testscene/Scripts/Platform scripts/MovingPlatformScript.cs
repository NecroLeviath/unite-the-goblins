using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    public Vector3 speed;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Scale(new Vector3(Mathf.PingPong(Time.time, 2), transform.position.y, transform.position.z), speed);
    }
}
