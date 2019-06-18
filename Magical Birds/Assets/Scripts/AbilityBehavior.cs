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
        jumpsLeft -= 1;
        GetComponent<PlayerMovementBehavior>().Jump(); 
    }

    public void Attack()
    {

    }

    public void ResetJumps()
    {
        jumpsLeft = extraJumps;
    }



}
