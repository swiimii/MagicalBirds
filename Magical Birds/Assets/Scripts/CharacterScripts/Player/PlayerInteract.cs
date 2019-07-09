using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject currentInterItem = null;

    void Update() {
        if(Input.GetButtonDown("Interact") && currentInterItem != null) {
            currentInterItem.SendMessage("DoInteract");
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Item")) {
            currentInterItem = other.gameObject;
        } else if(other.CompareTag("Item")) {
            //TODO: Pickup items logic
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Item")) {
            if(other.gameObject == currentInterItem){
                currentInterItem = null;
            }
        }
    }
}
