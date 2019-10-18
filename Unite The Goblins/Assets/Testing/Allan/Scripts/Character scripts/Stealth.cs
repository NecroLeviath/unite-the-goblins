using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth : MonoBehaviour
{
    //Renderer rend;
    Color color;
    PlayerSync ps;
    bool abilityUsed;
    bool useAbility;
    GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        //rend =  gameObject.GetComponentInChildren<Renderer>();
        //color = rend.material.color;
        ps = gameObject.transform.GetComponent<PlayerSync>();
        abilityUsed = false;
        useAbility = false;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "PlayerCharacter" && ps.ReturnStatus() == false)
        {
            if (useAbility)
            //if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("PS4_Triangle"))
            {
                gameObject.tag = "Stealth";
                //color.a = 0.2f;
                //rend.material.color = color;
                gameObject.transform.SendMessage("AbilityIsUsed", true);
                abilityUsed = true;
                foreach (GameObject enemy in enemies)
                {
                    Physics.IgnoreCollision(enemy.GetComponent<CapsuleCollider>(), GetComponent<Collider>());
                }
                useAbility = false;
            }
        }
        else if (gameObject.tag == "Stealth" && ps.ReturnStatus() == true && abilityUsed)
        {
            if (useAbility)
            //if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("PS4_Triangle"))
            {
                gameObject.tag = "PlayerCharacter";
                //color.a = 1.0f;
                //rend.material.color = color;
                gameObject.transform.SendMessage("AbilityIsUsed", false);
                abilityUsed = false;
                useAbility = false;
            }
        }
        useAbility = false;

        //Debug.Log(abilityUsed);

        //if (Input.GetKey(KeyCode.Joystick1Button3))
        //{
        //    gameObject.tag = "Stealth";
        //}
    }

    public void Message(string text)
    {
        if (text == "UseStealth")
        {
            useAbility = true;
        }
    }
}
