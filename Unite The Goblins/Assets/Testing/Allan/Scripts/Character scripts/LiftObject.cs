using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftObject : MonoBehaviour
{
    bool objectLifted;
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = null;
        objectLifted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            if (Input.GetKeyDown(KeyCode.E) && !objectLifted || Input.GetButtonDown("PS4_Square") && !objectLifted)
            {
                objectLifted = true;
                target.gameObject.transform.parent = transform;
                target.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                target.gameObject.GetComponent<Rigidbody>().useGravity = false;
                target.transform.position = transform.position;
                target.transform.Translate(0, 1.8f, 0);
                target.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            }
            else if (Input.GetKeyDown(KeyCode.E) && objectLifted || Input.GetButtonDown("PS4_Square") && objectLifted)
            {
                objectLifted = false;
                target.gameObject.transform.parent = null;
                target.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                target.gameObject.GetComponent<Rigidbody>().useGravity = true;
                target.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                target.transform.Translate(1, 0, 0);
                target = null;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Liftable")
        {
            target = other.gameObject;

            //if (Input.GetKeyDown(KeyCode.E) && !objectLifted || Input.GetButtonDown("PS4_Square") && !objectLifted)
            //{
            //    objectLifted = true;
            //    //other.gameObject.transform.parent = transform;
            //    other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //    other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            //    //other.transform.position.Set(transform.position.x, transform.position.y + 2, transform.position.z);
            //    other.transform.localPosition.Set(transform.position.x, transform.position.y + 2, transform.position.z);
            //    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            //}
            //else if (Input.GetKeyDown(KeyCode.E) && objectLifted || Input.GetButtonDown("PS4_Square") && objectLifted)
            //{
            //    objectLifted = false;
            //    other.gameObject.transform.parent = null;
            //    other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            //    other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!objectLifted)
        {
            target = null;
        }
    }
}
