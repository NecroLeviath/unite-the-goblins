using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletion : MonoBehaviour
{
    public CharacterManager characterManager;

    private int numPlayersFinished = 0;

    public string nextScene;

    void Start()
    {
        
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PlayerCharacter")
        {
            numPlayersFinished++;
        }

        if (characterManager.players.Count == numPlayersFinished)
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "PlayerCharacter")
        {
            numPlayersFinished--;
        }
    }
}
