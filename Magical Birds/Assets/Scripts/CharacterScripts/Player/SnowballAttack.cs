using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballAttack : RangedAttack
{
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        // Attack has zero knockback, and hits one enemy.
        // Dissipates after hitting the ground.

        if (collision.gameObject.GetComponent<ResourceController>())
        {
            collision.gameObject.GetComponent<ResourceController>().Damage(damage, Vector2.zero, 0);
        }
        

        EndAttack();
    }
}
