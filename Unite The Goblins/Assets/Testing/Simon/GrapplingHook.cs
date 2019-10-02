using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
	public float maxDistance = 10;
	public GameObject rope;
	private LineRenderer lr;
	GameObject target;
	bool attached = false;
	float distance;
	CharacterMotor cm;
	bool waitOneFrame = true;

	void Start()
	{
		lr = rope.GetComponent<LineRenderer>();
		cm = GetComponent<CharacterMotor>();
	}

	void Update()
	{
		var mouseDir = GetMouseDirection();

		// See if the player is targeting a hook
		if (attached)
		{
			var currentDist = Vector3.Distance(transform.position, target.transform.position);
			if (currentDist > 1)
			{
				var travelDir = (target.transform.position - transform.position).normalized;
				var speed = distance / 0.5f;
				transform.Translate(travelDir * speed * Time.deltaTime, Space.World);
				lr.SetPositions(new Vector3[] { transform.position, target.transform.position }); // Temp
				Debug.DrawLine(transform.position, transform.position + (travelDir * 3));
			}
			else if (waitOneFrame)
			{
				waitOneFrame = false;
				lr.SetPositions(new Vector3[] { new Vector3(), new Vector3() }); // Temp
				transform.position = target.transform.position + new Vector3(0, 0.8f, 0);
			}
			else
			{
				waitOneFrame = true;
				cm.enabled = true;
				attached = false;
			}
		}
		else if (Physics.Raycast(transform.position, mouseDir, out RaycastHit hit, maxDistance) && hit.collider.CompareTag("Hook"))
		{
			target = hit.collider.gameObject;
			// Add highlight to hook
		}
		else if (target != null) // See if the player stops targeting a hook
		{
			// Remove highlight from hook
			lr.SetPositions(new Vector3[] { new Vector3(), new Vector3() }); // Temp
			target = null;
		}

		if (target != null && !attached && Input.GetKeyDown(KeyCode.Mouse0) && target.transform.position.y > transform.position.y)
		{
			lr.SetPositions(new Vector3[] { transform.position, target.transform.position }); // Temp
			distance = Vector3.Distance(transform.position, target.transform.position);
			cm.enabled = false;
			attached = true;
		}
	}

	Vector3 GetMouseDirection()
	{
		var mousePos = Input.mousePosition;
		mousePos.z = 10;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);
		var playerPos = transform.position;
		return (mousePos - playerPos).normalized;
	}
}