using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateManager : MonoBehaviour {
    // Items Collected
    public List<GameObject> collectedItems;


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
        collectedItems = new List<GameObject>();

    }

    public void addCollectedItem(GameObject collectedItem){
        collectedItems.Add(collectedItem);
        Debug.Log("Added " + collectedItem + " to items collected");
    }

    public void setLevel(int levelId) {
        SceneManager.LoadScene(levelId);
    }

    private void ResetGameSession() {
        // TODO: make active level be whichever the main menu is
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
