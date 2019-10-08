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
			// Shut down player movement?
		}
		else if (Input.GetKeyDown(KeyCode.Q) && active)	// Removes all enemies from the affected list toggles off their "afraid" status
		{
			foreach (var e in affectedEnemies)
			{
				e.SendMessage("ToggleFear");
			}
			affectedEnemies.Clear();
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
				else if (distance <= radius)	// If an unaffected enemy enters the radius they become afraid
				{
					e.SendMessage("ToggleFear");
					affectedEnemies.Add(e);
				}
			}
		}

		#region Debug
		Gizmos.DrawSphere(transform.position, radius);
		#endregion
	}

	void EnemyClass()	// The code in this function should be in an enemy class
	{
		bool afraid = false;

		// If the enemy is afraid they should move away from the player

		/*public*/ void ToggleFear()	// Send the position of the player aswell?
		{
			afraid = !afraid;
		}
	}
}