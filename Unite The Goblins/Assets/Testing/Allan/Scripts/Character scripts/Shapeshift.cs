using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shapeshift : MonoBehaviour
{
    PlayerSync ps;
    CharacterMotor cm;
    public bool abilityUsed;
    bool useAbility;
    float timer = 0.02f;
    bool reset;

    // Start is called before the first frame update
    void Start()
    {
        ps = gameObject.GetComponentInParent<PlayerSync>();
        cm = gameObject.transform.GetComponent <CharacterMotor>();
        abilityUsed = false;
        useAbility = false;
        reset = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ps.ReturnStatus() == false)
        {
            if (useAbility)
            {
                //if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("PS4_O"))
                //{
                transform.gameObject.SendMessage("AbilityIsUsed", true);
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                abilityUsed = true;
                useAbility = false;
                //}
            }
        }
        else if(ps.ReturnStatus() == true && abilityUsed)
        {
            if (useAbility)
            {
                //if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("PS4_O"))
                //{
                cm.enabled = false;
                transform.gameObject.SendMessage("AbilityIsUsed", false);
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                abilityUsed = false;
                gameObject.transform.Translate(new Vector3(0, 1, 0));
                reset = true;
                useAbility = false;
                //}
            }
        }
        useAbility = false;
    }

    private void FixedUpdate()
    {
        if (reset)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                cm.enabled = true;
                timer = 0.02f;
                reset = false;
            }
        }
    }

    public void Message(string text)
    {
        if (text == "UseShapeshift")
        {
            useAbility = true;
        }
    }
}
