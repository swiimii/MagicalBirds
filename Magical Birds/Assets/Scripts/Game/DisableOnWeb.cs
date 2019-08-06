using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnWeb : MonoBehaviour
{
    public CSVScript target;

    void Start()
    {
        if (target)
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                target.enabled = false;
                return;
            }
        }

        else if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            gameObject.SetActive(false);
        }
    }
}
