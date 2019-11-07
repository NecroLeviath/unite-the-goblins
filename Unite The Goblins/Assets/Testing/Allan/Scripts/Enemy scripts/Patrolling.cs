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
	public float delay = 3.2f;

	bool afraid = false;	// For Scream ability

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
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);
        float step = speed * Time.deltaTime;
		if (target && target.tag == "Stealth")
		{
			target = null;
			foundPlayer = false;
		}

		if (afraid && target != null)	// For Scream ability
		{
			Vector3 playerPos = transform.position;
			playerPos.z = target.transform.position.z;
			var targetPos = transform.position + transform.position - playerPos;
			transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
		}
		else
		{
			if (foundPlayer && target != null)
			{
				Vector3 playerPos = transform.position;
				playerPos.z = target.transform.position.z;
                Vector3 targetPostition = new Vector3(this.transform.position.x,
                                        this.transform.position.y,
                                        target.transform.position.z);
                this.transform.LookAt(targetPostition);
                transform.position = Vector3.MoveTowards(transform.position, playerPos, step);
			}

			if (!foundPlayer && target == null)
			{
				Vector3 thisPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

                // Distance between current position and next position < allowence
                //if (Vector3.Distance(thisPos, points[destPoint].position) < allowence)
                //{
                //	UpdateTarget();
                //}

                if (Mathf.Abs(thisPos.z - points[destPoint].position.z) < allowence && Mathf.Abs(thisPos.y - points[destPoint].position.y) < 1f)
                {
                    UpdateTarget();
                }
                Vector3 targetPostition = new Vector3(this.transform.position.x,
                                        this.transform.position.y,
                                        points[destPoint].position.z);
                this.transform.LookAt(targetPostition);
                //transform.LookAt(points[destPoint], Vector3.back);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, points[destPoint].position.z), step);
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

    public void ToggleFear()	// For Scream ability
	{
		afraid = !afraid;
	}
}
