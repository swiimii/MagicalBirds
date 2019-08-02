using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityController>().enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; 
        GameIsPaused = false;
    }

    public void Pause()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityController>().enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<StateManager>().setScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

}
