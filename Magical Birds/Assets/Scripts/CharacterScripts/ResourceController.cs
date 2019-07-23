using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    public int maxHealth = 1;
    public int currentHealth;
    public float damageRecoilMagnitude = 120;

    public virtual void Start()
    {
        ResetHealth();
        var manager = FindObjectOfType<StateManager>();
        if (manager)
        {
            manager.player = gameObject;

        }
    }

    #region Damage Functions
    // There are two situations in which damage will have to be processed: When a source is given, or when a direction is given.
    // For zero recoil, use the latter with a magnitude of zero.
    public virtual void ProcessDamage(int damageDealt, Vector2 source)
    {
        currentHealth -= damageDealt;
        if (GetComponent<Rigidbody2D>())
        {
            DamageRecoil(source);
        }
        if (CheckDead())
        {
            StartCoroutine("Death");
        }
    }

    public virtual void ProcessDamage(int damageDealt, Vector2 direction, float magnitude)
    {
        currentHealth -= damageDealt;
        if (GetComponent<Rigidbody2D>())
        {
            FixedDamageRecoil(direction, magnitude);
        }

        if (CheckDead())
        {
            StartCoroutine("Death");
        }
    }

    public void Damage(int damageDealt, Vector2 source) // Called when damage is **received**
    {
        ProcessDamage(damageDealt, source); 
    }

    public void Damage(int damageDealt) // Source is below by default
    {
        ProcessDamage(damageDealt, transform.position - Vector3.down);
    }

    public void Damage(int damageDealt, Vector2 direction, float magnitude) // Used when utilizing fixed recoil
    {
        ProcessDamage(damageDealt, direction, magnitude);
    }

    protected virtual void DamageRecoil(Vector2 source) // Move character due to damage
    {
        Vector2 force = new Vector2(1, 1);
        if (Mathf.Abs(transform.position.x - source.x) <= 0.1f) // Move character up if the character is touching from above the enemy
        {
            force.x = 0;
        }

        else if (transform.position.x - source.x < 0) // Switch direction if character is on the other side of the source
        {
            force = new Vector2(-1, 1);
        }

        force = force / force.magnitude; // Set magnitude to 1

        GetComponent<Rigidbody2D>().velocity = force * damageRecoilMagnitude; // Move character away from object
    }

    protected virtual void FixedDamageRecoil(Vector2 direction, float magnitude)
    {
        GetComponent<Rigidbody2D>().velocity = direction * magnitude;
    }
    #endregion

    // Reset health. Could be used at checkpoints?
    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public virtual bool CheckDead()
    {
        return !(currentHealth > 0);
    }

    public virtual IEnumerator Death()
    {
        
        GetComponent<Collider2D>().enabled = false;
        if (GetComponent<Rigidbody2D>())
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
        GetComponent<MovementController>().enabled = false;
        GetComponent<Animator>().SetBool("isDead", true);
        transform.localScale = new Vector3(1, 1, 1);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
        
    }
}
