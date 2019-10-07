using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowCharacter : MonoBehaviour
{
    private bool isLifted;
    private GameObject target;

    public float ThrowForce = 25;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && target != null/* && !isLifted */&& target.GetComponent<Shapeshift>().abilityUsed)
        {
            //Debug.Log("lifting");
            //isLifted = true;
            //target.transform.position = transform.position;

            Vector3 shootDirection;
            shootDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            shootDirection.Normalize();

            target.GetComponent<CharacterMotor>().SetVelocity(new Vector3(0, shootDirection.y * ThrowForce, shootDirection.x * ThrowForce));

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
}
