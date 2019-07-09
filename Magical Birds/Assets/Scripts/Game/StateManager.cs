using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager
{
    private static StateManager instance;
    // Active Level
    private int activeLevel;
    // Maximum HP
    private int maxHP;
    // Current HP
    private int hp;
    // Damage Player Does
    private int dmg;
    // Items Collected
    private string[] collectedItems;
    // # of items collected, used to index
    private int itemsCollected;

    public static StateManager Instance{
        get{
            if(instance == null){
                instance = new StateManager();
            }

            return instance;
        }
    }

    // TODO: Remove later when there's something else to start it
    // Start is called before the first frame update
    void Start()
    {
        startState();
    }

    private void OnApplicationQuit() {
        instance = null;
    }

    public void startState(){
        Debug.Log("Creating new game state manager");

        //Set defaults
        activeLevel = 1;
        maxHP = 6;
        hp = 6;
        dmg = 2;
        itemsCollected = 0;
        collectedItems = new string[3];
    }

    public void addCollectedItem(string collectedItem){
        collectedItems[itemsCollected] = collectedItem;
        itemsCollected++;
        Debug.Log("Added " + collectedItem + " to items collected");
    }
}
