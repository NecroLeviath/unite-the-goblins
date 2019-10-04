using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenPointsGeneric : MonoBehaviour
{
    public Transform[] points;
    int destPoint;
    public float allowence = 0.1f;
    public float speed;
    float countDown;
    public float delay = 1.6f;

    // Use this for initialization
    void Start()
    {
        // Set first target
        countDown = 0;
        UpdateTarget();
    }

    // Update is called once per frame
    void Update()
    {
        // Update this position
        Vector3 thisPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);


        // Distance between current position and next position < allowence
        if (Vector3.Distance(thisPos, points[destPoint].position) < allowence)
        {
            UpdateTarget();
        }

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, points[destPoint].position, step);
    }

    void UpdateTarget()
    {
        countDown -= Time.deltaTime;

        if (countDown < 0)
        {
            if (points.Length == 0)
            {
                return;
            }
            transform.position = points[destPoint].position;
            destPoint = (destPoint + 1) % points.Length;
            countDown = delay;
        }
    }
}
