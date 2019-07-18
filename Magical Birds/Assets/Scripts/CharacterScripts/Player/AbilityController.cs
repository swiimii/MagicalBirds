using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{

    // Fields for the jump ability
    public bool canDoubleJump = false;

    // Fields for the attack abilities
    public bool readyToBasicAttack = true, readyToPowerAttack = true, readyToRangedAttack = true;
    public float basicAttackCooldown, powerAttackCooldown, rangedAttackCooldown;

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
        if (readyToBasicAttack && Input.GetButtonDown("Ability1"))
        {
            StartCoroutine("AttackDelay1");
            behav.BasicAttack();
        }

        if (readyToPowerAttack && Input.GetButtonDown("Ability2"))
        {
            // Attack Delay

            // Power Attack
        }

        if (readyToRangedAttack && Input.GetButtonDown("Ability3"))
        {
            StartCoroutine("AttackDelay3");
            behav.SnowballAttack();
        }


    }

    public IEnumerator AttackDelay1()
    {
        readyToBasicAttack = false;
        yield return new WaitForSeconds(basicAttackCooldown);
        readyToBasicAttack = true;
    }

    public IEnumerator AttackDelay2()
    {
        readyToPowerAttack = false;
        yield return new WaitForSeconds(powerAttackCooldown);
        readyToPowerAttack = true;
    }

    public IEnumerator AttackDelay3()
    {
        readyToRangedAttack = false;
        yield return new WaitForSeconds(rangedAttackCooldown);
        readyToRangedAttack = true;
    }



}
