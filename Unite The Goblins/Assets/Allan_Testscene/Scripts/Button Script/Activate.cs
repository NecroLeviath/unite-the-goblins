using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Activate : MonoBehaviour   
{
    public GameObject other;
    bool showText = false;
    GameObject text;
    bool collisionDetected;

    // Start is called before the first frame update
    void Start()
    {
        collisionDetected = false;
        text = new GameObject();
        text.transform.parent = gameObject.transform;
        text.transform.position = gameObject.transform.position;
        text.transform.Translate(-3, 0, 6);
        text.transform.Rotate(0, 0, 0);
        text.transform.localScale -= new Vector3(0.8f, 0.8f, 0.8f);
        TextMesh t = text.AddComponent<TextMesh>();
        t.text = "Press E or Square to DIE slowly and painfully";
        t.fontSize = 24;
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (collisionDetected)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("PS4_Square"))
            {
                ActivateFunction();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerCharacter")
        {
            text.SetActive(true);
            collisionDetected = true;
        }
    }

    void ActivateFunction()
    {
        other.SendMessage("IsActive");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerCharacter")
        {
            text.SetActive(false);
            collisionDetected = false;
        }
    }
}
