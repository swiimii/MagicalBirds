using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hummingbird : Friends
{
    protected override void Start() {
        currentQuest = "item";
        killRequirement = 8;
        itemToCollect = "BlueFeather";
        abilityLevel = 2;
        base.Start();
    }
}