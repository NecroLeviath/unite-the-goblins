using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformPoints : MonoBehaviour
{
    public Transform[] points;
    int destPoint;
    public float allowence = 0.1f;
    public float speed;
    float countDown;
    float delay = 3.2f;
    private GameObject target = null;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        // Set first target
        countDown = 0;
        target = null;
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

        //transform.position = Vector3.LerpUnclamped(transform.position, points[destPoint].position, Time.deltaTime);
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

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "PlayerCharacter" || col.gameObject.tag == "Box")
        {
            col.transform.parent = transform;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "PlayerCharacter" || col.gameObject.tag == "Box")
        {
            col.transform.parent = null;
        }
    }

    //void OnTriggerStay(Collider col)
    //{
    //    target = col.gameObject;
    //    offset = target.transform.position - transform.position;
    //}
    //void OnTriggerExit(Collider col)
    //{
    //    target = null;
    //}
    //void LateUpdate()
    //{
    //    if (target != null)
    //    {
    //        target.transform.position = transform.position + offset;
    //    }
    //}
}