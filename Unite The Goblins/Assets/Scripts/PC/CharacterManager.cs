﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> players;
    public Camera ChaseCam;
    PlayerController controller;
    int currentCharacter;
    
    void Start()
    {
        controller = transform.GetComponent<PlayerController>();
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("PlayerCharacter"));
        SetCurrentActiveCharacter(players[0]);
        CanvasUpdate(players[0]);
        currentCharacter = 0;
        controller.UpdateCurrentCharacter(ReturnCurrentCharacter());

        Physics.IgnoreCollision(players[0].GetComponent<Collider>(), players[1].GetComponent<Collider>());
        Physics.IgnoreCollision(players[0].GetComponent<Collider>(), players[2].GetComponent<Collider>());
        Physics.IgnoreCollision(players[1].GetComponent<Collider>(), players[2].GetComponent<Collider>());

    }

    private void SetCurrentActiveCharacter(GameObject player)
    {
        for (int i = 0; i != players.Count; ++i)
        {
            players[i].GetComponent<CharacterMotor>().inputMoveDirection = Vector2.zero;
            players[i].GetComponent<CharacterMotor>().inputJump = false;
            players[i].GetComponent<PlatformInputController>().enabled = false;
        }

        ChaseCam.GetComponent<SmoothCamera>().target = player.transform;
        player.GetComponent<PlatformInputController>().enabled = true;
    }

    public void CanvasUpdate(GameObject player)
    {
        GameObject guiTinker = GameObject.Find("/Gameplay-GUI Canvas/Ability-GUI/Tinker Abilities");
        GameObject guiStrong = GameObject.Find("/Gameplay-GUI Canvas/Ability-GUI/Strong Abilities");
        GameObject guiMobile = GameObject.Find("/Gameplay-GUI Canvas/Ability-GUI/Mobile Abilities");

        if (player.name == "TinkerCharacterWithMesh")
        {
            if (guiTinker != null) // Bara en extra check ifall gui inte finns i en annan scen, annars fungerar ej charactermanager
            {
                guiTinker.SetActive(true);
            }
        }
        if (player.name == "StrongCharacterWithMesh")
        {
            if (guiStrong != null)
            {
                guiStrong.SetActive(true);
            }
        }
        if (player.name == "MobileCharacterWithMesh")
        {
            if (guiMobile != null)
            {
                guiMobile.SetActive(true);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            SetCurrentActiveCharacter(players[0]);
            CanvasUpdate(players[0]);
            currentCharacter = 0;
            controller.UpdateCurrentCharacter(ReturnCurrentCharacter());
        }

        if (Input.GetKeyDown("2"))
        {
            SetCurrentActiveCharacter(players[1]);
            CanvasUpdate(players[1]);
            currentCharacter = 1;
            controller.UpdateCurrentCharacter(ReturnCurrentCharacter());
        }

        if (Input.GetKeyDown("3"))
        {
            SetCurrentActiveCharacter(players[2]);
            CanvasUpdate(players[2]);
            currentCharacter = 2;
            controller.UpdateCurrentCharacter(ReturnCurrentCharacter());
        }
    }

    public GameObject ReturnCurrentCharacter()
    {
        if (currentCharacter == 0)
        {
            return players[0];
        }
        else if (currentCharacter == 1)
        {
            return players[1];
        }
        else if (currentCharacter == 2)
        {
            return players[2];
        }
        else
        {
            return null;
        }
    }

    public void Die()
    {
        ChaseCam.gameObject.SetActive(true);
        SceneManager.LoadScene("DeathScene", LoadSceneMode.Additive);
    }
}