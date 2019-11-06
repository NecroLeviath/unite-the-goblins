using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scream : MonoBehaviour
{
	bool active = false; 
    float radius = 7;
	List<GameObject> enemies;
	List<GameObject> affectedEnemies;
    PlayerSync ps;
    public bool abilityUsed;
    bool useAbility;

    public bool Active { get => active;} // David : La till för åtkomst i CharacterAnimation (2019-11-06)

    void Start()
	{
		enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
		affectedEnemies = new List<GameObject>();
        ps = gameObject.transform.GetComponent<PlayerSync>();
        abilityUsed = false;
        useAbility = false;
    }

	void Update()
	{
        if (ps.ReturnStatus() == false)
        {
            if (/*Input.GetKeyDown(KeyCode.Q)*/ useAbility && !active) // Puts all enemies in range into the affected list and toggles on their "afraid" status
            {
                transform.gameObject.SendMessage("AbilityIsUsed", true);
                foreach (var e in enemies)
                {
                    if (Vector3.Distance(transform.position, e.transform.position) <= radius)
                    {
                        e.SendMessage("ToggleFear");
                        affectedEnemies.Add(e);
                    }
                }
                active = true;
                abilityUsed = true;
                useAbility = false;
                // Shut down player movement?
            }
        }
        else if (ps.ReturnStatus() == true && abilityUsed)
        {
            if (/*Input.GetKeyDown(KeyCode.Q)*/ useAbility && active) // Removes all enemies from the affected list toggles off their "afraid" status
            {
                transform.gameObject.SendMessage("AbilityIsUsed", false);
                foreach (var e in affectedEnemies)
                {
                    e.SendMessage("ToggleFear");
                }
                affectedEnemies.Clear();
                active = false;
                abilityUsed = false;
                useAbility = false;
            }
        }
        useAbility = false;

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

    public void Message(string text)
    {
        if (text == "UseScream")
        {
            useAbility = true;
        }
    }
}