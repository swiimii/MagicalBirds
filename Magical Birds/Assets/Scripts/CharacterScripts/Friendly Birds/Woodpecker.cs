using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodpecker : Friends
{
    protected override void Start() {
        base.Start();
        state = FindObjectOfType<StateManager>();
        currentQuest = "item";
        killRequirement = 4;
        itemToCollect = "GreenFeather";
        abilityLevel = 1;
    }
}