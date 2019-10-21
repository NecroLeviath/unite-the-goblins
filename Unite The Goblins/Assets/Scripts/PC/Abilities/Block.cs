using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    PlayerSync ps;
    bool active = false;
	public GameObject shieldPrefab;
	GameObject shield;
    public bool abilityUsed;
    bool useAbility;

    void Start()
	{
        ps = gameObject.transform.GetComponent<PlayerSync>();
        abilityUsed = false;
        useAbility = false;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        if (ps.ReturnStatus() == false)
        {
            if (useAbility)
            {
                //active = !active;
                if (shield == null) //active)
                {
                    transform.gameObject.SendMessage("AbilityIsUsed", true);

                    var offset = (transform.forward / 2) + new Vector3(0, 1f, 0); // In front of the goblin

                    shield = Instantiate(shieldPrefab, transform.position + offset, transform.rotation, transform);
                    abilityUsed = true;
                    useAbility = false;
                }
            }
        }
        else if (ps.ReturnStatus() == true && abilityUsed)
        {
            if (useAbility)
            {
                Destroy(shield);
                abilityUsed = false;
                useAbility = false;
                transform.gameObject.SendMessage("AbilityIsUsed", false);
            }
        }
        useAbility = false;
    }

    public void Message(string text)
    {
        if (text == "UseBlock")
        {
            useAbility = true;
        }
    }
}