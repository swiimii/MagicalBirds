using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{

    public StateManager sm;
    public GameObject checkpointButton;
    

    public void Start()
    {
        sm = GameObject.FindGameObjectWithTag("GameController").GetComponent<StateManager>();
 
        if (sm && sm.currentCheckpoint)
        {
            checkpointButton.SetActive(true);
        }        

    }

    public void Restart()
    {
        sm.currentCheckpoint = null;
        sm.setLevel(SceneManager.GetActiveScene().buildIndex);       
    }

    public void RestartFromCheckpoint()
    {
        sm.setLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        sm.currentCheckpoint = null;
        sm.setLevel(1);
    }
}
