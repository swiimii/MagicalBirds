using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBehavior : MonoBehaviour
{
    private readonly int extraJumps = 1;
    private int jumpsLeft;

    private void Start()
    {
        jumpsLeft = extraJumps;
    }

    public void ExtraJump()
    {
        jumpsLeft -= 1; // Remove a jump from "extraJumps". For double jumping, there should only be one available.
        GetComponent<PlayerMovementBehavior>().Jump(); 
    }

    public void Attack()
    {

    }

    public void ResetJumps()
    {
        if (jumpsLeft != extraJumps)
        {
            jumpsLeft = extraJumps;
        }
  
    }
    public bool HasJumpsRemaining()
    {
        return jumpsLeft > 0;
    }



}
