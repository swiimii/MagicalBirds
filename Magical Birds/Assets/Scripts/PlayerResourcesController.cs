using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourcesController : MonoBehaviour
{
    public int playerMaxHealth = 1;
    public int playerCurrentHealth;
    public float invulnerabilityTime = 2;
    public float damageRecoilMagnitude = 120;
    public bool isInvulnerable = false;
    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }

    // Call from attack scripts and enemy behavior scripts. When the player touhes
    public void Damage(int damageDealt, Vector2 source)
    {
        if(isInvulnerable)
        {
            return; // Don't do damage if the player is invulnerable
        }

        playerCurrentHealth -= damageDealt;
        StartCoroutine("Invulnerable");
        DamageRecoil(source);
    }

    public void Damage(int damageDealt) // Source is below by default
    {
        Damage(damageDealt, transform.position);
    }

    private void DamageRecoil(Vector2 source) // Move player due to damage
    {
        Vector2 force = new Vector2(1, 1);
        if (Mathf.Abs(transform.position.x - source.x) <= 0.1f) // Move player up if the player is touching from above the enemy
        {
            force.x = 0;
        }

        else if (transform.position.x - source.x < 0) // Switch direction if player is on the other side of the enemy
        {
            force = new Vector2(-1, 1);
        }

        force = force / force.magnitude; // Set magnitude to 1

        GetComponent<Rigidbody2D>().AddForce(force * damageRecoilMagnitude); // Move player away from object
    }

    public bool Heal(int healingDone)
    {
        // Player has max health, and can't heal any further. Player wasn't healed.
        if (playerCurrentHealth >= playerMaxHealth)
        {
            return false;
        }

        // Player is missing health, and can be healed.
        else
        {
            playerCurrentHealth += healingDone;
            if (playerCurrentHealth > playerMaxHealth) // Prevent overheal
            {
                playerCurrentHealth = playerMaxHealth;
            }
            return true;
        }
    }

    // Reset health. Could be used at checkpoints?
    public void ResetHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    // Coroutine which allows for invincibility frames. I think it'll be a good idea to allow the ...
    // player to move through enemies while invulnerable, IF we use more than one health. I forget what ...
    // we were going to do regarding health at the time of writing this comment
    public IEnumerator Invulnerable()
    {
        var mytime = invulnerabilityTime;
        isInvulnerable = true;
        while(mytime > 0)
        {
            mytime -= Time.deltaTime;
            yield return null;
        }
        isInvulnerable = false;
    }

    
}
