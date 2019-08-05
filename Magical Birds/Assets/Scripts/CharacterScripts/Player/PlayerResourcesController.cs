using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResourcesController : ResourceController
{
    // maxHealth, currentHealth, and damageRecoilMagnitude inherited from ResourceController
    public Color greyedOut = new Color(1, 1, 1, 0.5f);
    public Color full = new Color(1, 1, 1, 1.0f);

    public float invulnerabilityTime = 2;
    public bool isInvulnerable = false;
    public GameObject[] healthEggs;
    public GameObject[] damagedEggs;
    public GameObject[] feathers;
    public GameObject DeathScreen, HealthUI;
    private StateManager state;

    public override void Start()
    {
        base.Start();
        state = GameObject.FindGameObjectWithTag("GameController").GetComponent<StateManager>();
        state.player = gameObject;

        if(state.hasCheckpoint) {
            var friend = GameObject.FindGameObjectWithTag("Friend").transform.position;
            transform.position = new Vector3(friend.x, friend.y, transform.position.z);
        }
    }

    // Call from attack scripts and enemy behavior scripts. When the player gets hit
    public override void ProcessDamage(int damageDealt, Vector2 source)
    {
        if (isInvulnerable)
        {
            return; // Don't do damage if the player is invulnerable
        }

        else
        {
            StartCoroutine("PlayDamagedSound");
            StartCoroutine("Invulnerable");
            base.ProcessDamage(damageDealt, source);

            // Change UI according to current health
            if(currentHealth >= 0)
            {
                for (int i = healthEggs.Length; i > currentHealth; i--)
                {
                    damagedEggs[i - 1].SetActive(true);
                    healthEggs[i - 1].SetActive(false);
                    // healthEggs[i - 1].GetComponent<Image>().color = greyedOut;
                }
            } 

        }
        
    }

    public override void ProcessDamage(int damageDealt, Vector2 direction, float magnitude)
    {
        if (isInvulnerable)
        {
            return; // Don't do damage if the player is invulnerable
        }

        else
        {
            StartCoroutine("PlayDamagedSound");
            StartCoroutine("Invulnerable");
            base.ProcessDamage(damageDealt, direction, magnitude);

            // Change UI according to current health
            if (currentHealth > 0)
            {
                for (int i = healthEggs.Length; i > currentHealth; i--)
                {
                    damagedEggs[i - 1].SetActive(true);
                    healthEggs[i - 1].SetActive(false);
                }
            }

            else //Player has died
            {
                //Set all healthy eggs to damaged
                for (int i = healthEggs.Length; i > 0; i--)
                {
                    damagedEggs[i - 1].SetActive(true);
                    healthEggs[i - 1].SetActive(false);
                }

                // Start Death coroutine
                StartCoroutine("Death");
            }
        }
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

            // reset the health indicator
            for(int n = 0; n < currentHealth; n++)
            {
                // healthEggs[n].GetComponent<Image>().color = full;
                damagedEggs[n].SetActive(false);
                healthEggs[n].SetActive(true);
                //healthEggs[n].transform.position = temp;
            }
            return true;
        }
    }

    public IEnumerator PlayDamagedSound()
    {
        var sound = GetComponent<AudioSource>();
        if (!sound.isPlaying)
            sound.Play();
        else
            sound.UnPause();
        yield return new WaitForSeconds(.3f);
        sound.Pause();
    }

    public override IEnumerator Death()
    {
        // Appropriate death animation
        foreach (Collider c in GetComponents<Collider>())
        {
            c.enabled = false;
        }
        if (GetComponent<Rigidbody2D>())
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
        }

        GetComponent<MovementController>().enabled = false;
        GetComponent<Animator>().SetBool("isDead", true);
        transform.localScale = new Vector3(1, 1, 1);

        //Camera stops moving
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SimpleFollow>().player = null;
        GameObject.FindGameObjectWithTag("Background").GetComponent<BackgroundParallax>().trackedObject = null;

        yield return new WaitForSeconds(.5f);

        //Show death screen
        HealthUI.SetActive(false);
        DeathScreen.SetActive(true);


        
        


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
