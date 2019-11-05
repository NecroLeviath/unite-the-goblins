using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletion : MonoBehaviour
{

    private int numPlayersFinished = 0;

    public int numPlayersNeededToFinish = 3;

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

        if (numPlayersNeededToFinish == numPlayersFinished)
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
