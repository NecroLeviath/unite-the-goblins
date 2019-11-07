using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolNavmesh : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    GameObject target;
    bool foundPlayer;
    float countDown;
    public float delay = 3.2f;
    bool afraid = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = null;
        foundPlayer = false;
        countDown = delay;

        //GotoNextPoint();
    }


    void GotoNextPoint()
    {
        countDown -= Time.deltaTime;

        if (countDown < 0)
        {
            // Returns if no points have been set up
            if (points.Length == 0)
            {
                return;
            }

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            destPoint = (destPoint + 1) % points.Length;
            countDown = delay;
        }
    }


    void Update()
    {
        Debug.Log(destPoint);
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);

        if (target && target.tag == "Stealth")
        {
            target = null;
            foundPlayer = false;
        }

        if (afraid && target != null)   // For Scream ability
        {
            Vector3 playerPos = transform.position;
            playerPos.z = target.transform.position.z;
            var targetPos = transform.position + transform.position - playerPos;
            agent.destination = targetPos;
        }
        else
        {
            if (foundPlayer && target != null)
            {
                Vector3 targetPostition = new Vector3(this.transform.position.x,
                                        this.transform.position.y,
                                        target.transform.position.z);
                this.transform.LookAt(targetPostition);
                agent.destination = targetPostition;
            }

            if (!foundPlayer && target == null)
            {
                // Choose the next destination point when the agent gets
                // close to the current one.
                agent.destination = points[destPoint].position;
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    GotoNextPoint();
                }
                Vector3 targetPostition = new Vector3(this.transform.position.x,
                                        this.transform.position.y,
                                        points[destPoint].position.z);
                this.transform.LookAt(targetPostition);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerCharacter")
        {
            if (CanSeePlayer(other.gameObject) == true)
            {
                target = other.gameObject;
                foundPlayer = true;
            }
            else
            {
                target = null;
                foundPlayer = false;
            }
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

    bool CanSeePlayer(GameObject player)
    {
        float viewAngle = 110;
        bool inView, isVisible = false;
        RaycastHit hit;

        Vector3 pvec = player.transform.position - (transform.position + transform.up);
        float pangle = Vector3.Angle(transform.forward, pvec);
        inView = pangle < (viewAngle / 2.0f);

        if (inView && Physics.Raycast(transform.position + transform.up, pvec, out hit))
        {
            isVisible = hit.transform.gameObject.Equals(player);
        }
        return isVisible;
    }

    public void ToggleFear()    // For Scream ability
    {
        afraid = !afraid;
    }
}
