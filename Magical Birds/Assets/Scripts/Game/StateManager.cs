using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateManager : MonoBehaviour {
    private static StateManager instance;
    // Active Level
    [SerializeField] private int activeLevel;
    // Items Collected
    [SerializeField] private string[] collectedItems;
    // # of items collected, used to index
    [SerializeField] private int itemsCollected;

    private void Awake() {
        int numStateManagers = FindObjectsOfType<StateManager>().Length;
        
        if (numStateManagers > 1) {
            // TODO: Not sure where gameObject comes from in example, replace with what's appropriate
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() {
        // Set defaults
        // TODO: make active level be whichever the main menu is
        activeLevel = 0;
        itemsCollected = 0;
        collectedItems = new string[3];
    }

    public void addCollectedItem(string collectedItem){
        collectedItems[itemsCollected] = collectedItem;
        itemsCollected++;
        Debug.Log("Added " + collectedItem + " to items collected");
    }

    public void setLevel(string levelId) {
        activeLevel = levelId;
        SceneManager.LoadScene(levelId);
    }

    private void ResetGameSession() {
        // TODO: make active level be whichever the main menu is
        activeLevel = 0;
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
