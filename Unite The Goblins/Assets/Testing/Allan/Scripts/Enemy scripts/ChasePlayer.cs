using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    GameObject target;
    bool foundPlayer;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        target = null;
        foundPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (target && target.tag == "Stealth")
        {
            target = null;
            foundPlayer = false;
        }

        if (foundPlayer && target != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerCharacter")
        {
            target = other.gameObject;
            foundPlayer = true;

            Debug.Log("asdf");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerCharacter" || other.gameObject.tag == "Stealth")
        {
            target = null;
            foundPlayer = false;
        }
    }
}
