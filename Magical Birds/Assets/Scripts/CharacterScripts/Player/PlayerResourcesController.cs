using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourcesController : ResourceController
{
    // maxHealth, currentHealth, and damageRecoilMagnitude inherited from ResourceController
    public float invulnerabilityTime = 2;
    public bool isInvulnerable = false;

    // Call from attack scripts and enemy behavior scripts. When the player gets hit
    public override void ProcessDamage(int damageDealt, Vector2 source)
    {
        if(isInvulnerable)
        {
            return; // Don't do damage if the player is invulnerable
        }

        currentHealth -= damageDealt;
        StartCoroutine("Invulnerable");
        DamageRecoil(source);
    }

    public bool Heal(int healingDone)
    {
        // Player has max health, and can't heal any further. Player wasn't healed.
        if (currentHealth >= maxHealth)
        {
            return false;
        }

        // Player is missing health, and can be healed.
        else
        {
            currentHealth += healingDone;
            if (currentHealth > maxHealth) // Prevent overheal
            {
                currentHealth = maxHealth;
            }
            return true;
        }
    }

    

    // Coroutine which allows for invincibility frames. I think it'll be a good idea to allow the ...
    // player to move through enemies while invulnerable, IF we use more than one health. I forget what ...
    // we were going to do regarding health at the time of writing this comment
    public IEnumerator Invulnerable()
    {
        float flickerInterval = .10f; // interval between which the player sprite will flicker while invulnerable
        var countdown = invulnerabilityTime; // countdown timer, during which the player is invincible
        var spriteTransparency = GetComponent<SpriteRenderer>();
        bool flag = true;
        isInvulnerable = true;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);

        while(countdown > 0)
        {
            countdown -= flickerInterval;
            
            if(flag)
            {
                spriteTransparency.color = new Color(1, 1, 1, .5f);
                flag = false;
            }
            else
            {
                spriteTransparency.color = Color.white;

                flag = true;
            }
            // Due to this line, the "flickerInterfal" is time estimate, which will be 0-.10 seconds ... 
            // longer than the actual invulnerability time
            yield return new WaitForSeconds(flickerInterval); 
        }
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        spriteTransparency.color = Color.white;
        isInvulnerable = false;
    }

    
}
