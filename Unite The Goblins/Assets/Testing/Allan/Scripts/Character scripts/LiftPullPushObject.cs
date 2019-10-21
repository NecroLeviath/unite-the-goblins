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
    PlayerSync ps;
    public bool abilityUsed;
    bool useAbility;

    // Start is called before the first frame update
    void Start()
    {
        target = null;
        objectLifted = false;
        liftable = false;
        pushable = false;
        pushing = false;
        ps = gameObject.transform.GetComponent<PlayerSync>();
        abilityUsed = false;
        useAbility = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (target && ps.ReturnStatus() == false)
        {
            //if (Input.GetKeyDown(KeyCode.E)|| Input.GetButtonDown("PS4_Square"))
            if (useAbility)
            {
                if (!objectLifted && liftable)
                {
                    transform.gameObject.SendMessage("AbilityIsUsed", true);
                    objectLifted = true;
                    target.gameObject.transform.parent = transform;
                    target.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    target.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    target.transform.position = transform.position;
                    target.transform.Translate(0, target.transform.localScale.y * 1.4f, 0);
                    target.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                    abilityUsed = true;
                    useAbility = false;
                }
                else if (!objectLifted && !pushing && pushable)
                {
                    transform.gameObject.SendMessage("AbilityIsUsed", true);
                    pushing = true;
                    abilityUsed = true;
                    useAbility = false;
                }
            }
        }
        else if (ps.ReturnStatus() == true && abilityUsed)
        {
            if (useAbility)
            {
                if (objectLifted && liftable)
                {
                    transform.gameObject.SendMessage("AbilityIsUsed", false);
                    objectLifted = false;
                    target.gameObject.transform.parent = null;
                    target.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    target.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    target.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    target.transform.Translate(0, 0, target.transform.localScale.z * 1f);
                    target = null;
                    abilityUsed = false;
                    useAbility = false;
                }
                else if (!objectLifted && pushing && pushable)
                {
                    transform.gameObject.SendMessage("AbilityIsUsed", false);
                    pushing = false;
                    abilityUsed = false;
                    useAbility = false;
                }
            }
        }
        float strength = power * Time.deltaTime;
        if (pushable && !objectLifted && pushing)
        {
            Vector3 directionVector;
            directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            target.transform.Translate(0, 0, (strength * directionVector.x));
        }
        useAbility = false;
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
            if (abilityUsed)
            {
                transform.gameObject.SendMessage("AbilityIsUsed", false);
            }
            target = null;
            liftable = false;
            pushable = false;
            pushing = false;
            abilityUsed = false;
            useAbility = false;
        }
    }

    public void Message(string text)
    {
        if (text == "UseLiftandpush")
        {
            useAbility = true;
        }
    }
}
