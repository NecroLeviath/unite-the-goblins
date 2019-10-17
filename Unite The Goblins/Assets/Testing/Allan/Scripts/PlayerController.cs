using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool abilityInUse;
    CharacterManager cm;
    GameObject g;

    // Start is called before the first frame update
    void Start()
    {
        abilityInUse = false;
        cm = transform.GetComponent<CharacterManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (g == null)
        {
            g = cm.ReturnCurrentCharacter();
        }

        if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("PS4_O"))
        {
            g.SendMessage("Message", "UseShapeshift");
        }
    }

    public void AbilityIsUsed(bool b)
    {
        abilityInUse = b;
    }

    public bool ReturnStatus()
    {
        return abilityInUse;
    }
}
