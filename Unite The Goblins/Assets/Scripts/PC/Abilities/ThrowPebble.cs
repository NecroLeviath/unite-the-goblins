using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPebble : MonoBehaviour
{
    public GameObject pebblePrefab;
    PlayerSync ps;
    public bool abilityUsed;
    bool useAbility;
    void Start()
    {
        ps = gameObject.transform.GetComponent<PlayerSync>();
        abilityUsed = false;
        useAbility = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ps.ReturnStatus() == false)
        {
            if (useAbility)
            {
                //if (Input.GetKeyDown(KeyCode.R))
                //{
                transform.gameObject.SendMessage("AbilityIsUsed", true);
                var go = Instantiate(pebblePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

                Vector3 shootDirection;
                shootDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
                shootDirection.Normalize();

                go.GetComponent<Rigidbody>().velocity = new Vector3(0, shootDirection.y * 10.0f, shootDirection.x * 10.0f);

                abilityUsed = true;
                useAbility = false;
                //}
            }
            transform.gameObject.SendMessage("AbilityIsUsed", false);
            abilityUsed = false;
            useAbility = false;
        }
        useAbility = false;
    }

    public void Message(string text)
    {
        if (text == "UseThrowPebble")
        {
            useAbility = true;
        }
    }
}
