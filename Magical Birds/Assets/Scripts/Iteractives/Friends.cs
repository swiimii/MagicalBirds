using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friends : Interactives
{
    public bool talkedTo = false;
    public bool itemReturned = false;
    public bool enemiesKilled = false;
    public string currentQuest; 
    public StateManager state;
    public string itemToCollect;
    public int killRequirement;
    public int killCount = 0;
    public int abilityLevel;
    public void incrementKilled() { 
        if(talkedTo && currentQuest == "kill") {
            killCount++;
        }
    }

    public void QuestCheck() {
        if(itemReturned && enemiesKilled) {
            // TODO: do the level win
        } else if (itemReturned) {
            currentQuest = "kill";
            // TODO: switch the quest reminder
        } else if (enemiesKilled) {
            currentQuest = "item";
            // TODO: switch the quest reminder
        }
    }

    public override void DoInteract(){
        if(!talkedTo) {
            talkedTo = true;
            QuestCheck();
        }

        switch (currentQuest)
        {
            case "item": {
                foreach (GameObject item in state.collectedItems) {
                    if(item.name == itemToCollect) {
                        itemReturned = true;
                        state.collectedItems.Remove(item);
                    }
                }

                if (itemReturned) {
                    if(state.unlockedAbilities < abilityLevel) {
                        state.unlockedAbilities = abilityLevel;
                    }
                }
                QuestCheck();
                break;
            }
            case "kill": {
                if (killCount >= killRequirement) {
                    enemiesKilled = true;
                }
                QuestCheck();
                break;
            }
            default:
                QuestCheck();
                break;
        }
    }
}
