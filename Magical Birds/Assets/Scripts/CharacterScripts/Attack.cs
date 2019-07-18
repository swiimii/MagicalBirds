using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage = 1;
    public List<GameObject> hits;
    // Only hit each enemy once (this is helpful if an enemy has more than one hitbox)
    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        foreach(GameObject g in hits)
        {
            if (other.gameObject.Equals(g))
            {
                return;
            }
            hits.Add(other.gameObject);
        }
    }
    public virtual void EndAttack()
    {
        Destroy(gameObject);
    }
}
