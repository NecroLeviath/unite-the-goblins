using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPebble : MonoBehaviour
{
    public GameObject pebblePrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            var go = Instantiate(pebblePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

            Vector3 shootDirection;
            shootDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            shootDirection.Normalize();

            go.GetComponent<Rigidbody>().velocity = new Vector3(0, shootDirection.y * 10.0f, shootDirection.x * 10.0f);
        }
    }
}
