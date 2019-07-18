using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveItemBase : MonoBehaviour
{
    public virtual void DoInteract(){
        FindObjectOfType<StateManager>().addCollectedItem(gameObject);
        gameObject.SetActive(false);

    }
}
