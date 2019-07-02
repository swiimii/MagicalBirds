using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{

    // Update is called once per frame
    public bool CanDoubleJump = false;
    void Update()
    {
        var grounded = GetComponent<PlayerMovementController>().grounded;
        var behav = GetComponent<AbilityBehavior>();

        if (grounded) // Reset double jumps if the player is grounded
        {
            GetComponent<AbilityBehavior>().ResetJumps();
        }
        else if (CanDoubleJump)
        {
            if (Input.GetButtonDown("Jump") && behav.HasJumpsRemaining()) // If airborne, check if player is pressing jump
            {
                behav.ExtraJump(); // Double jump if button is pressed
            }
        }
    }
}
