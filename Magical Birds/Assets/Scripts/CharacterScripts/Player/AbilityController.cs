using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{

    // Fields for the jump ability
    public bool canDoubleJump = false;

    // Fields for the basic attack ability
    public bool readyToAttack = true;
    public float attackCooldown;

    void Update()
    {
        var grounded = GetComponent<PlayerMovementController>().grounded;
        var behav = GetComponent<AbilityBehavior>();

        // Jumping control
        if (grounded) // Reset double jumps if the player is grounded
        {
            GetComponent<AbilityBehavior>().ResetJumps();
        }
        else if (canDoubleJump)
        {
            if (Input.GetButtonDown("Jump") && behav.HasJumpsRemaining()) // If airborne, check if player is pressing jump
            {
                behav.ExtraJump(); // Double jump if button is pressed
            }
        }

        // Attack control
        if (readyToAttack && Input.GetButtonDown("Ability1"))
        {
            StartCoroutine("AttackDelay");
            behav.Attack();
        }


    }

    public IEnumerator AttackDelay()
    {
        readyToAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        readyToAttack = true;
    }



}
