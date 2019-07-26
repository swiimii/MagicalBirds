using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{

    // Fields for the jump ability
    public bool canDoubleJump = false;

    // Fields for the attack abilities
    public bool readyToBasicAttack = true, readyToPowerAttack = false, readyToRangedAttack = false;
    public float basicAttackCooldown, powerAttackCooldown, rangedAttackCooldown;
    public GameObject basicAttackCDSprite, powerAttackCDSprite, rangedAttackCDSprite;

    private void checkAbilities() {
        int unlocked = FindObjectOfType<StateManager>().unlockedAbilities;
        
        switch (unlocked) {
            case 0: {
                readyToPowerAttack = false;
                readyToRangedAttack = false;
                canDoubleJump = false;
                break;
            }
            case 1: {
                readyToPowerAttack = true;
                readyToRangedAttack = false;
                canDoubleJump = false;
                break;
            }
            case 2: {
                readyToPowerAttack = true;
                readyToRangedAttack = false;
                canDoubleJump = true;
                break;
            }
            case 3: {
                readyToPowerAttack = true;
                readyToRangedAttack = true;
                canDoubleJump = true;
                break;
            }
            default: {
                readyToPowerAttack = false;
                readyToRangedAttack = false;
                canDoubleJump = false;
                break;
            }
        }
    }

    private void Start() {
        checkAbilities();
    }

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
        if (readyToBasicAttack && Input.GetButton("Ability1"))
        {
            StartCoroutine("AttackDelay1");
            behav.BasicAttack();
        }

        if (readyToPowerAttack && Input.GetButton("Ability2"))
        {
            StartCoroutine("AttackDelay2");
            behav.HeavyAttack();
        }

        if (readyToRangedAttack && Input.GetButton("Ability3"))
        {
            StartCoroutine("AttackDelay3");
            behav.SnowballAttack();
        }


    }

    public IEnumerator AttackDelay1()
    {
        readyToBasicAttack = false;
        basicAttackCDSprite.SetActive(true);
        var sprite = basicAttackCDSprite.GetComponent<SpriteRenderer>();
        var maxSpriteSize = sprite.bounds.size.y;

        
        for(float i = 0; i < basicAttackCooldown; i += Time.deltaTime)
        {
            sprite.size = new Vector2(sprite.size.x, maxSpriteSize - i / basicAttackCooldown * maxSpriteSize);
            yield return null;
        }

        sprite.size = new Vector2(sprite.size.x, maxSpriteSize);

        basicAttackCDSprite.SetActive(false);
        readyToBasicAttack = true;
    }

    public IEnumerator AttackDelay2()
    {
        readyToPowerAttack = false;
        powerAttackCDSprite.SetActive(true);
        var sprite = powerAttackCDSprite.GetComponent<SpriteRenderer>();
        var maxSpriteSize = sprite.bounds.size.y;

        
        for (float i = 0; i < powerAttackCooldown; i += Time.deltaTime)
        {
            sprite.size = new Vector2(sprite.size.x, maxSpriteSize - i / powerAttackCooldown * maxSpriteSize);
            yield return null;
        }

        sprite.size = new Vector2(sprite.size.x, maxSpriteSize);

        powerAttackCDSprite.SetActive(false);
        readyToPowerAttack = true;
    }

    public IEnumerator AttackDelay3()
    {
        readyToRangedAttack = false;
        rangedAttackCDSprite.SetActive(true);
        var sprite = rangedAttackCDSprite.GetComponent<SpriteRenderer>();
        var maxSpriteSize = sprite.bounds.size.y;

        
        for (float i = 0; i < rangedAttackCooldown; i += Time.deltaTime)
        {
            sprite.size = new Vector2(sprite.size.x, maxSpriteSize - i / rangedAttackCooldown * maxSpriteSize);
            yield return null;
        }

        sprite.size = new Vector2(sprite.size.x, maxSpriteSize);

        rangedAttackCDSprite.SetActive(false);
        readyToRangedAttack = true;
    }



}
