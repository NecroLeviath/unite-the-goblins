using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftPullPushObject : MonoBehaviour
{
    bool objectLifted;
    GameObject target;
    bool liftable;
    bool pushable;
    bool pushing;
    float power = 5;

    // Start is called before the first frame update
    void Start()
    {
        target = null;
        objectLifted = false;
        liftable = false;
        pushable = false;
        pushing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            if (Input.GetKeyDown(KeyCode.E)|| Input.GetButtonDown("PS4_Square"))
            {
                if (!objectLifted && liftable)
                {
                    objectLifted = true;
                    target.gameObject.transform.parent = transform;
                    target.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    target.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    target.transform.position = transform.position;
                    target.transform.Translate(0, target.transform.localScale.y * 1.5f, 0);
                    target.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                }
                else if (objectLifted && liftable)
                {
                    objectLifted = false;
                    target.gameObject.transform.parent = null;
                    target.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    target.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    target.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    target.transform.Translate(0, 0, target.transform.localScale.z * 4f);
                    target = null;
                }
                else if (!objectLifted && !pushing && pushable)
                {
                    pushing = true;
                }
                else if (!objectLifted && pushing && pushable)
                {
                    pushing = false;
                }
            }

            float strength = power * Time.deltaTime;
            if (pushable && !objectLifted && pushing)
            {
                Vector3 directionVector;
                directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
                target.transform.Translate((strength * directionVector.x), 0, 0);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Liftable")
        {
            target = other.gameObject;
            liftable = true;
            pushable = false;
        }
        else if (other.gameObject.tag == "Pushable")
        {
            target = other.gameObject;
            liftable = false;
            pushable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!objectLifted || pushing)
        {
            target = null;
            liftable = false;
            pushable = false;
        }
    }
}
