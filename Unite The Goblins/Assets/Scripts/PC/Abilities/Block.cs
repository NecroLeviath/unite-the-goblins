using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	bool active = false;
	public GameObject shieldPrefab;
	GameObject shield;
	
	void Start()
	{
		
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			//active = !active;
			if (shield == null) //active)
			{

				var offset = (transform.forward / 2) + new Vector3(0, 1f, 0); // In front of the goblin
				
				shield = Instantiate(shieldPrefab, transform.position + offset, transform.rotation, transform);
			}
			else
			{
				Destroy(shield);
			}
		}
	}
}