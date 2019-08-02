using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Interactives
{
    public override void DoInteract(){
        FindObjectOfType<PlayerResourcesController>().SendMessage("Heal", 1);
        Destroy(gameObject);
    }
}
