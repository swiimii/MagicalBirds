using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodpecker : Friends
{
    private void Start() {
        state = FindObjectOfType<StateManager>();
        currentQuest = "item";
        killRequirement = 4;
        itemToCollect = "GreenFeather";
        abilityLevel = 1;
    }
}