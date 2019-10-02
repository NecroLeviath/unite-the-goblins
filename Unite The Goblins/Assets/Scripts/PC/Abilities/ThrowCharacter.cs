using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowCharacter : MonoBehaviour
{
    public GameObject tinkererCharacter;

    private bool isLifted;
    private GameObject target;

    public float ThrowForce = 25;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && target != null && !isLifted && tinkererCharacter.GetComponent<Shapeshift>().abilityUsed)
        {
            isLifted = true;
            target.transform.gameObject.transform.parent = transform;
        }
        else if (Input.GetKeyDown(KeyCode.Z) && isLifted)
        {
            Vector3 shootDirection;
            shootDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            shootDirection.Normalize();

            target.GetComponent<CharacterMotor>().SetVelocity(new Vector3(shootDirection.x * ThrowForce, shootDirection.y * ThrowForce, 0));

            isLifted = false;
            target.gameObject.transform.parent = null;
            target = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        target = other.gameObject;
    }
}
