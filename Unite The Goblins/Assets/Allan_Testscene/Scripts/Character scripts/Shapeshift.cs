using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shapeshift : MonoBehaviour
{
    PlayerSync ps;
    CharacterMotor cm;
    bool abilityUsed;
    float timer = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        ps = gameObject.transform.parent.GetComponent<PlayerSync>();
        cm = gameObject.transform.GetComponent <CharacterMotor>();
        abilityUsed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ps.ReturnStatus() == false)
        {
            if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("PS4_O"))
            {
                gameObject.transform.parent.SendMessage("AbilityIsUsed", true);
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                abilityUsed = true;
            }
        }
        else if(ps.ReturnStatus() == true && abilityUsed)
        {
            if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("PS4_O"))
            {
                cm.enabled = false;
                gameObject.transform.parent.SendMessage("AbilityIsUsed", false);
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                abilityUsed = false;
                gameObject.transform.Translate(new Vector3(0, 1, 0));
            }
        }
    }

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            cm.enabled = true;
            timer = 0.02f;
        }
    }
}
