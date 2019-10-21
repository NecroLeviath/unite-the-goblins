using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSync : MonoBehaviour
{
    public bool abilityInUse;

    // Start is called before the first frame update
    void Start()
    {
        abilityInUse = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(abilityInUse);
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
