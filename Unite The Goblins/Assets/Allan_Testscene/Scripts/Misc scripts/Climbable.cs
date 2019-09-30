using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbable : MonoBehaviour
{
    GameObject player;
    bool canClimb;
    bool isClimbing;
    float climbSpeed;
    CharacterMotor cm;

    // Start is called before the first frame update
    void Start()
    {
        canClimb = false;
        climbSpeed = 1.5f;
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
            }
            else if (v < 0)
            {
                cm.enabled = false;
                player.transform.Translate(new Vector3(0, -climbSpeed, 0) * Time.deltaTime);
                isClimbing = true;
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
            canClimb = false;
            isClimbing = false;
            cm.enabled = true;
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
