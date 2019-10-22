using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceExplosive : MonoBehaviour
{
    public GameObject explosivePrefab;
    private bool explosiveActive;
    private GameObject explosiveObj;
    PlayerSync ps;
    bool useAbility;

    void Start()
    {
        ps = gameObject.transform.GetComponent<PlayerSync>();
        useAbility = false;
    }

    void Update()
    {
        if (ps.ReturnStatus() == false)
        {
            if (useAbility)
            {
                //if (Input.GetKeyDown(KeyCode.F) || Input.GetButtonDown("PS4_Triangle"))
                //{
                transform.gameObject.SendMessage("AbilityIsUsed", true);
                useAbility = false;
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
                //}
                transform.gameObject.SendMessage("AbilityIsUsed", false);
            }
        }
        useAbility = false;
    }

    public void Message(string text)
    {
        if (text == "UseExplosive")
        {
            useAbility = true;
        }
    }
}
