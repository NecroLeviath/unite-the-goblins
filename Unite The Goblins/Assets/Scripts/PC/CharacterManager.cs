using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject[] players;
    public Camera ChaseCam;
    
    void Start()
    {
        SetCurrentActiveCharacter(players[0]);
    }

    private void SetCurrentActiveCharacter(GameObject player)
    {
        for (int i = 0; i != players.Length; ++i)
        {
            players[i].GetComponent<CharacterMotor>().inputMoveDirection = Vector2.zero;
            players[i].GetComponent<CharacterMotor>().inputJump = false;
            players[i].GetComponent<PlatformInputController>().enabled = false;
        }

        ChaseCam.GetComponent<SmoothCamera>().target = player.transform;
        player.GetComponent<PlatformInputController>().enabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            SetCurrentActiveCharacter(players[0]);
        }

        if (Input.GetKeyDown("2"))
        {
            SetCurrentActiveCharacter(players[1]);
        }

        if (Input.GetKeyDown("3"))
        {
            SetCurrentActiveCharacter(players[2]);
        }
    }
}