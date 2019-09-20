using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject[] players;
    public Camera camera;
    
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

        camera.GetComponent<SmoothCamera>().target = player.transform;
        player.GetComponent<PlatformInputController>().enabled = true;
    }

    void Update()
    {
        if (Input.GetButtonDown("Player1"))
        {
            SetCurrentActiveCharacter(players[0]);
        }

        if (Input.GetButtonDown("Player2"))
        {
            SetCurrentActiveCharacter(players[1]);
        }

        if (Input.GetButtonDown("Player3"))
        {
            SetCurrentActiveCharacter(players[2]);
        }
    }
}