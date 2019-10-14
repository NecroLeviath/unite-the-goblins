using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falldown : MonoBehaviour
{
    bool isActive = false;
    //bool hasKilledPlayer = false;
    Rigidbody rb;
    public bool isTriggerActivated;
    public float fallDelay;
    float fallTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fallTimer = fallDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && !isTriggerActivated)
        {
            rb.useGravity = true;
        }
        else if (isActive && isTriggerActivated)
        {
            fallTimer -= Time.deltaTime;
            if (fallTimer < 0)
            {
                rb.useGravity = true;
                fallTimer = fallDelay;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (isTriggerActivated && other.gameObject.tag == "PlayerCharacter")
        //{
        //    IsActive();
        //}

        //if (!hasKilledPlayer && collision.gameObject.tag == "PlayerCharacter")
        //{
        //    collision.gameObject.SendMessage("Death");
        //}
        //hasKilledPlayer = true;
    }

    void IsActive()
    {
        isActive = true;
    }
}
