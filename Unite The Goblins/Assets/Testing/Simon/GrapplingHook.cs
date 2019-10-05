using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
	public float maxDistance = 10;
	public GameObject rope;
	public float velocity = 30;
	LineRenderer lr;
	GameObject target;
	bool isCeilingHook;
	bool attached = false;
	float distance;
	CharacterMotor cm;
	bool waitOneFrame = true;
	float outVelocity;

	void Start()
	{
		lr = rope.GetComponent<LineRenderer>();
		cm = GetComponent<CharacterMotor>();
	}

	void Update()
	{
		var mouseDir = GetMouseDirection();
		
		if (attached)
		{
			var currentDist = Vector3.Distance(transform.position, target.transform.position);
			if (!isCeilingHook && currentDist > 1 || isCeilingHook && currentDist > 0.1)
			{
				var travelDir = (target.transform.position - transform.position).normalized;
				var dist = Time.deltaTime * velocity > currentDist ? currentDist / Time.deltaTime : velocity;	// Makes sure the player doesn't overshoot
				transform.Translate(travelDir * dist * Time.deltaTime, Space.World);
				lr.SetPositions(new Vector3[] { transform.position, target.transform.position });	// Temp (Draw a rope between the player and the hook)
			}
			else if (waitOneFrame)	// Wait a frame because of the Character Motor
			{
				waitOneFrame = false;
				lr.SetPositions(new Vector3[] { new Vector3(), new Vector3() });    // Temp (Remove the rope between the player and the hook)
				if (!isCeilingHook) transform.position = target.transform.position + new Vector3(0, 0.5f, 0);	// Place the player on the hook
			}
			else
			{
				waitOneFrame = true;
				cm.enabled = true;
				if (isCeilingHook) cm.SetVelocity(new Vector3(outVelocity, 0, 0));
				attached = false;
			}
		}
		else if (Physics.Raycast(transform.position, mouseDir, out RaycastHit hit, maxDistance))	// See if the player is targeting a hook
		{
			if (hit.collider.CompareTag("LedgeHook"))
			{
				target = hit.collider.gameObject;
				isCeilingHook = false;
			}
			else if (hit.collider.CompareTag("CeilingHook"))
			{
				target = hit.collider.gameObject;
				isCeilingHook = true;
			}
			// Add highlight to hook
		}
		else if (target != null)	// See if the player stops targeting a hook
		{
			// Remove highlight from hook
			target = null;
		}

		if (target != null && !attached && Input.GetKeyDown(KeyCode.Mouse0) && target.transform.position.y > transform.position.y)
		{
			lr.SetPositions(new Vector3[] { transform.position, target.transform.position });   // Temp (Draw a rope between the player and the hook)
			distance = Vector3.Distance(transform.position, target.transform.position);
			cm.enabled = false;
			if (isCeilingHook) outVelocity = GetOutVelocity(transform.position, target.transform.position);
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

	float GetOutVelocity(Vector3 playerPosition, Vector3 hookPosition)
	{
		var d = (hookPosition.x - playerPosition.x) / 2;	// Distance between hook and endpoint
		var h = playerPosition.y - hookPosition.y;      // Distance between hook and ground
		var a = -cm.movement.gravity;   // Gravity
		var dir = d / Mathf.Abs(d);	// Direction
		var v = ((2 * d) - (2 * h * dir)) / (2 * Mathf.Sqrt(2 * h / a));  // Horizontal velocity
		return v;
	}
}