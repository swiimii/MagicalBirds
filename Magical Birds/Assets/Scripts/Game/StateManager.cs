using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour {
    // Player
    public GameObject player;
    // Items Collected
    public List<GameObject> collectedItems;
    // Unlocked Levels
    public int unlockedLevels;
    /* 
      Unlocked Abilities:
        0 - None
        1 - Smash Attack
        2 - Smash + Double Jump
        3 - Double + Smash + Snowball
    */
    public int unlockedAbilities;
    // Player has a checkpoint to respawn from?
    public bool hasCheckpoint;
    public float masterVolume;
    public float musicVolume;
    public float effectsVolume;

    [SerializeField] volatile bool gameLoaded = false;

    private void Awake() {
        int numStateManagers = FindObjectsOfType<StateManager>().Length;
        
        if (numStateManagers > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        ReadData();
        setScene(1);
    }  

    public void addCollectedItem(GameObject collectedItem){
        collectedItems.Add(collectedItem);

        for(int n = 0; n < player.GetComponent<PlayerResourcesController>().feathers.Length; n++){
            player.GetComponent<PlayerResourcesController>().feathers[n].GetComponent<FeatherUI>().changeActiveFeather(collectedItem.name);
        }

        Debug.Log("Added " + collectedItem + " to items collected");
    }

    public void removeCollectedItem(GameObject collectedItem) {
        collectedItems.Remove(collectedItem);

        for(int n = 0; n < player.GetComponent<PlayerResourcesController>().feathers.Length; n++){
            player.GetComponent<PlayerResourcesController>().feathers[n].GetComponent<FeatherUI>().changeActiveFeather();
        }

        Debug.Log("Removed " + collectedItem + " to items collected");
    }

    public void setScene(int sceneId) {
        var resetCheckpoint = true;
        if(hasCheckpoint && sceneId == SceneManager.GetActiveScene().buildIndex) {
            resetCheckpoint = false;
        }

        SceneManager.LoadScene(sceneId);
        Time.timeScale = 1f;
        player = GameObject.FindGameObjectWithTag("Player");
        
        if(resetCheckpoint){
            hasCheckpoint = false;
        }
    }

    private void ResetGameSession() {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void SaveProgress() {
        GetComponent<CSVScript>().SaveGameState();
    }

    public void OnApplicationQuit() {
        SaveProgress();
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
    }

    public void SetSoundsVolume(float volume)
    {
        effectsVolume = volume;
    }

    public void ReadData()
    {
        // Set defaults
        collectedItems = new List<GameObject>();

        // Read from save file
        List<string>[] data = GetComponent<CSVScript>().ReadFile();
        unlockedLevels = Convert.ToInt32(data[1][0]);
        unlockedAbilities = Convert.ToInt32(data[1][1]);
        masterVolume = (float)Convert.ToDouble(data[1][2]);
        musicVolume = (float)Convert.ToDouble(data[1][3]);
        effectsVolume = (float)Convert.ToDouble(data[1][4]);

        gameLoaded = true;     

    }


}
