﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public int ID;
    GameObject target;
    bool collisionDetected;
    public bool needsKey;

    // Start is called before the first frame update
    void Start()
    {
        collisionDetected = false;
        target = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (collisionDetected)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("PS4_Square"))
            {
                ActivateFunction();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerCharacter")
        {
            collisionDetected = true;
            target = other.gameObject;
        }
    }

    public void ActivateFunction()
    {
        Inventory inv = target.GetComponent<Inventory>();
        if (inv.ReturnKey(ID) && needsKey)
        {
            gameObject.SetActive(false);
        }
        else if (!needsKey)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerCharacter")
        {
            collisionDetected = false;
            target = null;
        }
    }
}
