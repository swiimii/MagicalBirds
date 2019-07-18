using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItems : Interactives
{
    public override void DoInteract(){
        print("Interacting with " + name);
        FindObjectOfType<StateManager>().addCollectedItem(gameObject);
        gameObject.SetActive(false);
    }
}
