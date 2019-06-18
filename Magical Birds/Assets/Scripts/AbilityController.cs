﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PlayerMovementController>().grounded)
        {
            GetComponent<AbilityBehavior>().ResetJumps();
        }
    }
}
