using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{

    public StateManager sm;

    public GameObject victoryScreen;

    public void Start()
    {
        sm = GameObject.FindGameObjectWithTag("GameController").GetComponent<StateManager>();        
    }

    public void ReturnToMenu()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<StateManager>().setScene(1);
    }

    public void Continue()
    {
        sm.setScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator Victory()
    {
        yield return new WaitForSeconds(.5f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityController>().enabled = false;
        victoryScreen.SetActive(true);
        Time.timeScale = 0f;
        sm.unlockedLevels = SceneManager.GetActiveScene().buildIndex - 1;    
    }
}
