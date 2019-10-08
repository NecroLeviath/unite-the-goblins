using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scream : MonoBehaviour
{
	bool active = false;
	float radius = 7;
	List<GameObject> enemies;
	List<GameObject> affectedEnemies;

	void Start()
	{
		enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
		affectedEnemies = new List<GameObject>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q) && !active)	// Puts all enemies in range into the affected list and toggles on their "afraid" status
		{
			foreach (var e in enemies)
			{
				if (Vector3.Distance(transform.position, e.transform.position) <= radius)
				{
					e.SendMessage("ToggleFear");
					affectedEnemies.Add(e);
				}
			}
			active = true;
			// Shut down player movement?
		}
		else if (Input.GetKeyDown(KeyCode.Q) && active)	// Removes all enemies from the affected list toggles off their "afraid" status
		{
			foreach (var e in affectedEnemies)
			{
				e.SendMessage("ToggleFear");
			}
			affectedEnemies.Clear();
			active = false;
		}

		if (active)
		{
			foreach (var e in enemies)
			{
				var distance = Vector3.Distance(transform.position, e.transform.position);
				if (affectedEnemies.Contains(e) && distance > radius)	// If an affected enemy leaves the radius they stop being afraid
				{
					e.SendMessage("ToggleFear");
					affectedEnemies.Remove(e);
				}
				else if (!affectedEnemies.Contains(e) && distance <= radius)	// If an unaffected enemy enters the radius they become afraid
				{
					e.SendMessage("ToggleFear");
					affectedEnemies.Add(e);
				}
			}
		}
	}
}