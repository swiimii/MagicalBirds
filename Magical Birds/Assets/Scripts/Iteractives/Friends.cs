using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friends : Interactives
{
    public bool talkedTo = false;
    public GameObject itemToCollect;
    public int abilityLevel;
    public override void DoInteract(){
        print("Interacting with " + name);
        StateManager state = FindObjectOfType<StateManager>();

        if(!talkedTo) {
            talkedTo = true;
            // TODO: Do dialogue asking the player to do a task
        } else if(state.collectedItems.Contains(itemToCollect)){
            if(state.unlockedAbilities < abilityLevel) {
                state.unlockedAbilities = abilityLevel;
            }
        }
    }
}
