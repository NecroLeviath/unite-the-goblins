using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject g;

    // Start is called before the first frame update
    void Start()
    {
        g = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (g)
        {
            if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("PS4_O"))
            {
                g.SendMessage("Message", "UseShapeshift");
            }
            else if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("PS4_Triangle"))
            {
                g.SendMessage("Message", "UseStealth");
                //g.SendMessage("Message", "Jumplikeafrog");
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("PS4_R2"))
            {
                g.SendMessage("Message", "UseRun");
            }
        }
    }

    public void UpdateCurrentCharacter(GameObject character)
    {
        g = character;
    }
}
