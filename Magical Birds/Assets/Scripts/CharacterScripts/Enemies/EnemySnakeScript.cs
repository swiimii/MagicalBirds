using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySnakeScript : MonoBehaviour
{
    public int damageDealt = 1;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerResourcesController>().Damage(damageDealt, transform.position);//, transform.position);
        }
    }

}
