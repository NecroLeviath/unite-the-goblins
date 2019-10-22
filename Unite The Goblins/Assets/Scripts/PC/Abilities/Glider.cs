using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glider : MonoBehaviour
{
    private CharacterMotor cm;
    private float standardFallspeed;
    public float glideFallSpeed;
    private bool isActive = false;
    PlayerSync ps;
    public bool abilityUsed;
    bool useAbility;

    void Start()
    {
        cm = GetComponent<CharacterMotor>();
        standardFallspeed = cm.movement.maxFallSpeed;
        ps = gameObject.transform.GetComponent<PlayerSync>();
        abilityUsed = false;
        useAbility = false;
    }

    void Update()
    {
        if (ps.ReturnStatus() == false)
        {
            if (useAbility)
            {
                if ((/*Input.GetKeyDown(KeyCode.E) && !GetComponent<CharacterMotor>().grounded) || (Input.GetButtonDown("PS4_O") && */!GetComponent<CharacterMotor>().grounded))
                {
                    transform.gameObject.SendMessage("AbilityIsUsed", true);
                    abilityUsed = true;
                    useAbility = false;
                    isActive = true;
                }
            }
        }
        else if (ps.ReturnStatus() == true && abilityUsed)
        {
            if (useAbility)
            {
                transform.gameObject.SendMessage("AbilityIsUsed", false);
                abilityUsed = false;
                useAbility = false;
                isActive = false;
            }
        }
        useAbility = false;

        if (GetComponent<CharacterMotor>().grounded)
        {
            isActive = false;
        }

        if (isActive)
        {
            GetComponent<CharacterMotor>().movement.maxFallSpeed = glideFallSpeed;
        }
        else
        {
            GetComponent<CharacterMotor>().movement.maxFallSpeed = standardFallspeed;
        }
    }

    public void Message(string text)
    {
        if (text == "UseGlider")
        {
            useAbility = true;
        }
    }
}
