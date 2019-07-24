using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour {
    // Player
    public GameObject player;
    // Items Collected
    public List<GameObject> collectedItems;
    // Unlocked Levels
    public List<int> unlockedLevels;
    // TODO: Move to player?
    /* 
      Unlocked Abilities:
        0 - None
        1 - Smash Attack
        2 - Smash + Double Jump
        3 - Double + Smash + Snowball
    */
    public int unlockedAbilities;
    // Player's last used checkpoint to respawn from
    // TODO: should be game object?
    public GameObject currentCheckpoint;
    public float masterVolume;
    public float musicVolume;
    public float soundVolume;


    private void Awake() {
        int numStateManagers = FindObjectsOfType<StateManager>().Length;
        
        if (numStateManagers > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() {
        // Set defaults
        // TODO: get from save if there is one
        collectedItems = new List<GameObject>();
        unlockedAbilities = 0;
    }

    public void addCollectedItem(GameObject collectedItem){
        collectedItems.Add(collectedItem);
        Debug.Log("Added " + collectedItem + " to items collected");
    }

    public void setLevel(int levelId) {
        SceneManager.LoadScene(levelId);
    }

    private void ResetGameSession() {
        // TODO: change this LoadScene arg to be whatever the main menu scene is
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void DoPlayerDeath() {
        // TODO: whatever happens when the player dies, i.e. reset to checkpoint
    }

    private void SaveProgress() {
        // TODO: do saving to csv;
    }

    private void OnApplicationQuit() {
        // TODO: do saving to csv;
    }
}
