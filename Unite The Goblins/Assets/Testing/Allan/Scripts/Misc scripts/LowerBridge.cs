using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerBridge : MonoBehaviour
{
    bool isActive;
    public int rotateSpeed = 2;
    public float smooth = 1f;
    bool lowerBridge = false;
    Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        targetRotation = transform.rotation;
        targetRotation *= Quaternion.AngleAxis(90, new Vector3(1, 0, 0));
    }

    private void Update()
    {
        if (isActive/* && gameObject.transform.rotation.eulerAngles.x >= 0 && gameObject.transform.rotation.x < 90.0f*/)
        {
            //gameObject.transform.Rotate(Time.deltaTime * rotateSpeed, 0, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * smooth * Time.deltaTime);
        }

        //Debug.Log(gameObject.transform.rotation.eulerAngles.x);
    }

    void IsActive()
    {
        isActive = true;
    }
}
