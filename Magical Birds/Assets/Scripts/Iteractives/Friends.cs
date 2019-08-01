using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friends : Interactives
{
    public bool talkedTo = false;
    public GameObject textBubble;
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

    protected virtual void Start() {
        state = FindObjectOfType<StateManager>();
        textBubble.GetComponent<Animator>().SetInteger("currentQuest", 0);
    }

    public void QuestCheck() {
        if(itemReturned && enemiesKilled) {
            textBubble.GetComponent<Animator>().SetInteger("currentQuest", 3);
            // TODO: do the level win
        } else if (itemReturned) {
            currentQuest = "kill";
            textBubble.GetComponent<Animator>().SetInteger("currentQuest", 2);
        } else if (enemiesKilled) {
            currentQuest = "item";
            textBubble.GetComponent<Animator>().SetInteger("currentQuest", 1);
        }
    }

    public override void DoInteract(){
        if(!talkedTo) {
            talkedTo = true;
            switch (currentQuest) {
                case "item": {
                    textBubble.GetComponent<Animator>().SetInteger("currentQuest", 1);
                    break;
                }
                case "kill": {
                    textBubble.GetComponent<Animator>().SetInteger("currentQuest", 2);
                    break;
                }
                default:
                    break;
            }
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
