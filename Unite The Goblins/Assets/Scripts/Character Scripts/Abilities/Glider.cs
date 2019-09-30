using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glider : MonoBehaviour
{
    private CharacterMotor cm;
    private float standardFallspeed;
    public float glideFallSpeed;
    private bool isActive = false;

    void Start()
    {
        cm = GetComponent<CharacterMotor>();
        standardFallspeed = cm.movement.maxFallSpeed;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !GetComponent<CharacterMotor>().grounded)
        {
            isActive = !isActive;
        }

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
}
