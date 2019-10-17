using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<int> keyList;

    // Start is called before the first frame update
    void Start()
    {
        keyList = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (int i in keyList)
        {
            Debug.Log(i);
        }
    }

    void AddKey(int ID)
    {
        keyList.Add(ID);
    }

    public bool ReturnKey(int ID)
    {
        foreach (int i in keyList)
        {
            if (i == ID)
            {
                return true;
            }
        }
        return false;
    }
}
