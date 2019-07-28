using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherUI : MonoBehaviour
{
    public void changeActiveFeather(string featherName = null){
        if(featherName == null) {
            gameObject.SetActive(false);
            return;
        }

        if(gameObject.name == (featherName + "UI")) {
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }
}
    
