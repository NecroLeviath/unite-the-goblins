using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> players;
    public Camera ChaseCam;
    
    void Start()
    {
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("PlayerCharacter"));
        SetCurrentActiveCharacter(players[0]);

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

    public void Die()
    {
        ChaseCam.gameObject.SetActive(true);
        SceneManager.LoadScene("DeathScene", LoadSceneMode.Additive);
    }
}