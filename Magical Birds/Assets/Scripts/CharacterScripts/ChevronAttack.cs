using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChevronAttack : Attack
{
    public List<GameObject> hits;
    public int damage;

    // I set the physics settings so that objects on the "Player Attack" layer only hit enemies.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Only hit each enemy once
        foreach (GameObject hit in hits)
        {
            if (collision.gameObject == hit)
            {
                return;
            }
        }

        // Hit the enemy
        hits.Add(collision.gameObject);
        collision.gameObject.GetComponent<EnemyResourceController>().Damage(damage, transform.position);
    }
}
