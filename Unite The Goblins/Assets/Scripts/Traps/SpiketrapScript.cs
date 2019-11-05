using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiketrapScript : MonoBehaviour
{
    public bool instantKill;

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PlayerCharacter")
        {
            if (instantKill)
            {
                GameObject.Find("Character Manager").GetComponent<CharacterManager>().Die();
            }
            else
            {
                collision.gameObject.GetComponent<Health>().TakeDamage();
            }
        }
    }
}
