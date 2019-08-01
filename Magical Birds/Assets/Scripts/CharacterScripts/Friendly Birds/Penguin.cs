using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penguin : Friends
{
    protected override void Start() {
        base.Start();
        state = FindObjectOfType<StateManager>();
        currentQuest = "item";
        killRequirement = 5;
        itemToCollect = "YellowFeather";
        abilityLevel = 3;
    }
}