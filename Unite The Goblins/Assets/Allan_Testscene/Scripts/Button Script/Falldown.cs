using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falldown : MonoBehaviour
{
    bool isActive = false;
    bool hasKilledPlayer = false;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            rb.useGravity = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasKilledPlayer && collision.gameObject.tag == "PlayerCharacter")
        {
            collision.gameObject.SendMessage("Death");
        }
        hasKilledPlayer = true;
    }

    void IsActive()
    {
        isActive = true;
    }
}
