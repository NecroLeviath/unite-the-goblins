﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbable : MonoBehaviour
{
    GameObject player;
    bool canClimb;
    bool isClimbing;
    int dir;
    float climbSpeed;
    CharacterMotor cm;
    bool reset;
    float timer = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        canClimb = false;
        climbSpeed = 1.5f;
        dir = -1;
        reset = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canClimb)
        {
            float v = Input.GetAxis("Vertical");
            if (v > 0)
            {
                cm.enabled = false;
                player.transform.Translate(new Vector3(0, climbSpeed, 0) * Time.deltaTime);
                isClimbing = true;
                dir = 1;
            }
            else if (v < 0)
            {
                cm.enabled = false;
                player.transform.Translate(new Vector3(0, -climbSpeed, 0) * Time.deltaTime);
                isClimbing = true;
                dir = 0;
            }
            else if (v == 0 && isClimbing)
            {
                cm.enabled = false;
            }
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "PlayerCharacter")
        {
            player = collision.gameObject;
            cm = player.transform.GetComponent<CharacterMotor>();
            canClimb = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "PlayerCharacter")
        {
            if (dir == 1)
            {
                player.transform.position = transform.GetChild(0).transform.position;
            }
            canClimb = false;
            isClimbing = false;
            //cm.enabled = true;
            dir = -1;
            reset = true;
        }
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

    //private void OnTriggerStay(Collider collision)
    //{
    //    if (collision.gameObject.tag == "PlayerCharacter")
    //    {
    //        canClimb = true;
    //    }
    //}

    //private void OnTriggerExit(Collider collision)
    //{
    //    if (collision.gameObject.tag == "PlayerCharacter")
    //    {
    //        cm.enabled = true;
    //        canClimb = false;
    //    }
    //}
}
