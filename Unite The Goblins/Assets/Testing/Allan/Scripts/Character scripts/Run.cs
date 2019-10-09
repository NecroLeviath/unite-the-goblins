using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour
{
    PlayerSync ps;
    CharacterMotor cm;
    bool abilityUsed;
    float standardSpeed;

    // Start is called before the first frame update
    void Start()
    {
        ps = gameObject.transform.GetComponent<PlayerSync>();
        cm = gameObject.transform.GetComponent<CharacterMotor>();
        abilityUsed = false;
        standardSpeed = cm.movement.ReturnSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if (ps.ReturnStatus() == false)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("PS4_R2"))
            {
                gameObject.transform.SendMessage("AbilityIsUsed", true);
                cm.movement.SetSpeed(standardSpeed * 1.8f);
                abilityUsed = true;
            }
        }
        else if (ps.ReturnStatus() == true && abilityUsed)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("PS4_R2"))
            {
                gameObject.transform.SendMessage("AbilityIsUsed", false);
                cm.movement.SetSpeed(standardSpeed);
                abilityUsed = false;
            }
        }
    }
}
