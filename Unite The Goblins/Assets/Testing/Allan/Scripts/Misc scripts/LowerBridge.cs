using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerBridge : MonoBehaviour
{
    bool isActive;
    float rotateSpeed = 25.0f;
    bool lowerBridge = false;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
    }

    private void Update()
    {
        if (isActive && gameObject.transform.rotation.eulerAngles.x >= 0 && gameObject.transform.rotation.eulerAngles.x < 89.9f)
        {
            gameObject.transform.Rotate(Time.deltaTime * rotateSpeed, 0, 0);
        }

        Debug.Log(gameObject.transform.rotation.eulerAngles.x);
    }

    void IsActive()
    {
        isActive = true;
    }
}
