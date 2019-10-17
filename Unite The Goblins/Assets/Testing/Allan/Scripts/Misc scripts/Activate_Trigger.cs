using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate_Trigger : MonoBehaviour
{
    public GameObject other;
    public bool ownTrigger;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerCharacter")
        {
            if (!ownTrigger)
            {
                ActivateFunction();
            }
            else
            {
                transform.BroadcastMessage("IsActive");
            }
        }
    }

    public void ActivateFunction()
    {
        //other.SendMessage("IsActive");
        other.BroadcastMessage("IsActive");
    }
}
