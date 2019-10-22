using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowCharacter : MonoBehaviour
{
    private bool isLifted;
    private GameObject target;
    PlayerSync ps;
    bool useAbility;

    public float ThrowForce = 25;

    void Start()
    {
        ps = gameObject.transform.GetComponent<PlayerSync>();
        useAbility = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ps.ReturnStatus() == false)
        {
            if (/*Input.GetKeyDown(KeyCode.Z)*/useAbility && target != null/* && !isLifted */&& target.GetComponent<Shapeshift>().abilityUsed)
            {
                //Debug.Log("lifting");
                //isLifted = true;
                //target.transform.position = transform.position;
                transform.gameObject.SendMessage("AbilityIsUsed", true);
                Vector3 shootDirection;
                shootDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
                shootDirection.Normalize();

                target.GetComponent<CharacterMotor>().SetVelocity(new Vector3(0, shootDirection.y * ThrowForce, shootDirection.x * ThrowForce));

                useAbility = false;
                transform.gameObject.SendMessage("AbilityIsUsed", false);
            }
            //else if (Input.GetKeyDown(KeyCode.Z) && isLifted)
            //{
            //    Debug.Log("throwing");
            //    Vector3 shootDirection;
            //    shootDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            //    shootDirection.Normalize();

            //    target.GetComponent<CharacterMotor>().SetVelocity(new Vector3(0, shootDirection.y * ThrowForce, shootDirection.x * ThrowForce));

            //    isLifted = false;
            //    target.gameObject.transform.parent = null;
            //    target = null;
            //}
        }
        useAbility = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerCharacter" && other.GetComponent<Shapeshift>() != null)
            target = other.gameObject;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerCharacter" && other.GetComponent<Shapeshift>() != null)
            target = null;
    }

    public void Message(string text)
    {
        if (text == "UseThrowcharacter")
        {
            useAbility = true;
        }
    }
}
