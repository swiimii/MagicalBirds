using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveItemBase : MonoBehaviour
{
    public string itemName;
    public Sprite image;
    public virtual void DoInteract(){
        gameObject.SetActive(false);
        StateManager.Instance.addCollectedItem(itemName);
    }
}
