using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PebbleScript : MonoBehaviour
{
    Activate buttonActivate;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Button")
        {
            ((Activate)collider.gameObject.GetComponent(typeof(Activate))).ActivateFunction();
        }

        Destroy(gameObject);
    }
}
