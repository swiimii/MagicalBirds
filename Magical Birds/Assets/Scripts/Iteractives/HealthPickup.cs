using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Interactives
{
    public override void DoInteract(){
        print("Interacting with " + name);
        FindObjectOfType<PlayerResourcesController>().SendMessage("Heal", 1);
        gameObject.SetActive(false);
    }
}
