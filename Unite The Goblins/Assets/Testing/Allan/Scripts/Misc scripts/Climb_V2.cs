using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb_V2 : MonoBehaviour
{
    GameObject player;
    public List<GameObject> cubeList = new List<GameObject>();
    bool canClimb;
    CharacterMotor cm;
    bool reset;
    float timer = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        canClimb = false;
        reset = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canClimb && player != null)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("PS4_Square"))
            {
                cm.enabled = false;
                canClimb = false;
                if (player.transform.position.y < cubeList[0].transform.position.y)
                {
                    player.transform.position = cubeList[1].transform.position;
                }
                else
                {
                    if (player.transform.position.z < cubeList[0].transform.position.z)
                    {
                        player.transform.position = cubeList[3].transform.position;
                    }
                    else
                    {
                        player.transform.position = cubeList[2].transform.position;
                    }
                }
                reset = true;
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
            player = null;
            cm = null;
            canClimb = false;
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
}
