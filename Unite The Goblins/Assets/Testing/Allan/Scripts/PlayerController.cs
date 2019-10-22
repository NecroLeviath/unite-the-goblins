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
                g.SendMessage("Message", "UseThrowcharacter");
                g.SendMessage("Message", "UseThrowPebble");
            }
            else if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("PS4_Triangle"))
            {
                g.SendMessage("Message", "UseStealth");
                g.SendMessage("Message", "UseBlock");
                g.SendMessage("Message", "UseExplosive");
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("PS4_R2"))
            {
                g.SendMessage("Message", "UseRun");
                g.SendMessage("Message", "UseLiftandpush");
                g.SendMessage("Message", "UseScream");
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                g.SendMessage("Message", "UseGrapplinghook");
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                g.SendMessage("Message", "UseGlider");
            }
            else if (Input.GetButtonDown("PS4_X"))
            {
                g.SendMessage("Message", "UseGrapplinghook");
                g.SendMessage("Message", "UseGlider");
            }
        }
    }

    public void UpdateCurrentCharacter(GameObject character)
    {
        g = character;
    }
}
