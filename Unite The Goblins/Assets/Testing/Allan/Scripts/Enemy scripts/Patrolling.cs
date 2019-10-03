using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour
{
    GameObject target;
    bool foundPlayer;
    public float speed;

    public Transform[] points;
    int destPoint;
    public float allowence = 0.1f;
    float countDown;
    float delay = 3.2f;

    // Start is called before the first frame update
    void Start()
    {
        target = null;
        foundPlayer = false;
        countDown = 0;
        UpdateTarget();
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        if (target && target.tag == "Stealth")
        {
            target = null;
            foundPlayer = false;
        }

        if (foundPlayer && target != null)
        {
            Vector3 playerPos = transform.position;
            playerPos.x = target.transform.position.x;
            transform.position = Vector3.MoveTowards(transform.position, playerPos, step);
        }

        if (!foundPlayer && target == null)
        {
            Vector3 thisPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            // Distance between current position and next position < allowence
            if (Vector3.Distance(thisPos, points[destPoint].position) < allowence)
            {
                UpdateTarget();
            }

            transform.position = Vector3.MoveTowards(transform.position, points[destPoint].position, step);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerCharacter")
        {
            target = other.gameObject;
            foundPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerCharacter" || other.gameObject.tag == "Stealth")
        {
            target = null;
            foundPlayer = false;
        }
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
            //transform.position = points[destPoint].position;
            destPoint = (destPoint + 1) % points.Length;
            countDown = delay;
        }
    }
}
