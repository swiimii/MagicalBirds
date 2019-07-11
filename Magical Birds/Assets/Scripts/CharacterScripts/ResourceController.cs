using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourceController : MonoBehaviour
{
    public int maxHealth = 1;
    public int currentHealth;
    public float damageRecoilMagnitude = 120;

    public virtual void Damage(int damageDealt, Vector2 source) // Called when damage is **received**
    {
        currentHealth -= damageDealt;
        DamageRecoil(source);
    }

    public virtual void Damage(int damageDealt) // Source is below by default
    {
        Damage(damageDealt, transform.position);
    }

    protected virtual void DamageRecoil(Vector2 source) // Move player due to damage
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

        GetComponent<Rigidbody2D>().velocity = force * damageRecoilMagnitude; // Move player away from object
    }
}
