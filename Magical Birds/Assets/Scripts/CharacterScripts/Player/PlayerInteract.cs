using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject currentItem = null;
    public GameObject currentFriend = null;
    public GameObject interactPrompt = null;

    void Update() {
        if(Input.GetButtonDown("Interact")) {
            if(currentFriend != null) {
                currentFriend.SendMessage("DoInteract");
                return;
            }
            if(currentItem != null) {
                currentItem.SendMessage("DoInteract");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Item")) {
            interactPrompt.SetActive(true);
            currentItem = other.gameObject;
        } else if (other.CompareTag("Friend")) {
            interactPrompt.SetActive(true);
            currentFriend = other.gameObject;
        } else if(other.CompareTag("Pickup")) {
            other.SendMessage("DoPickup");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Item") || other.CompareTag("Friend")) {
            interactPrompt.SetActive(false);
            if(other.gameObject == currentItem){
                currentItem = null;
            }

            if(other.gameObject == currentFriend){
                currentFriend = null;
            }
        }
    }
}
