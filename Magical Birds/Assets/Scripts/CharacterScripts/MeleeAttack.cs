using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeAttack : Attack
{
    public float hitboxDuration = 0;
    public float spriteDuration = .25f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        StartCoroutine("AttackFade");
    }

    protected IEnumerator AttackFade()
    {
        yield return new WaitForSeconds(hitboxDuration);
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(spriteDuration - hitboxDuration);
        EndAttack();
    }

    // I set the physics settings so that objects on the "Player Attack" layer only hit enemies.
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //Only hit each enemy once
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);

        // Damage must be set in override. That way, different attacks can use different types of knockback / recoil
        //collision.gameObject.GetComponent<ResourceController>().Damage(damage, transform.position);
    }

    protected float GetDirection()
    {
        return Mathf.Abs(transform.localScale.x)/transform.localScale.x;
    }

}
