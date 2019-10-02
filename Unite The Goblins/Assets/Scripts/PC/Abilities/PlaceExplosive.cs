using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceExplosive : MonoBehaviour
{
    public GameObject explosivePrefab;
    private bool explosiveActive;
    private GameObject explosiveObj;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) || Input.GetButtonDown("PS4_Triangle"))
        {
            if (!explosiveActive)
            {
                explosiveObj = Instantiate(explosivePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f), Quaternion.identity);
            }
            else
            {
                var objects = GameObject.FindGameObjectsWithTag("DestroyableBlock");
                foreach (var obj in objects)
                {
                    if (obj.GetComponent<BoxCollider>().bounds.Intersects(explosiveObj.GetComponent<SphereCollider>().bounds))
                        Destroy(obj);
                }
                Destroy(explosiveObj);
            }

            explosiveActive = !explosiveActive;
        }
    }
}
