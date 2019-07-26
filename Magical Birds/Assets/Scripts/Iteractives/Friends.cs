using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friends : Interactives
{
    public bool talkedTo = false;
    public Animator textBubble;
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
        } else {

            switch (currentQuest) {
                case "item": {
                    foreach (GameObject item in state.collectedItems) {
                        if(item.name == itemToCollect) {
                            itemReturned = true;
                            state.removeCollectedItem(item);
                            break;
                        }
                    }

                    if (itemReturned) {
                        if(state.unlockedAbilities < abilityLevel) {
                            state.unlockedAbilities = abilityLevel;
                            FindObjectOfType<AbilityController>().SendMessage("checkAbilities");
                        }
                    }
                    break;
                }
                case "kill": {
                    if (killCount >= killRequirement) {
                        enemiesKilled = true;
                    }
                    break;
                }
                default:
                    break;
            }

        }

        QuestCheck();
    }
}
