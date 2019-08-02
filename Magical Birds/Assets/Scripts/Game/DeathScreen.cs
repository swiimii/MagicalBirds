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
 
        if (sm && sm.hasCheckpoint)
        {
            checkpointButton.SetActive(true);
        }        

    }

    public void Restart()
    {
        sm.hasCheckpoint = false;
        sm.setScene(SceneManager.GetActiveScene().buildIndex);       
    }

    public void RestartFromCheckpoint()
    {
        sm.setScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        sm.hasCheckpoint = false;
        sm.setScene(1);
    }
}
