using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hummingbird : Friends
{
    private void Start() {
        state = FindObjectOfType<StateManager>();
        currentQuest = "item";
        killRequirement = 10;
        itemToCollect = "BlueFeather";
        abilityLevel = 2;
    }
}