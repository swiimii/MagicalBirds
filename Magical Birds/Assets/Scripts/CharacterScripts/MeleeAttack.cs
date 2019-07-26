using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeAttack : Attack
{
    public float hitboxDuration = .25f;
    public float spriteDuration = 0;

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
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        // Prevent colliders from sticking
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);

        // Damage must be set in override. That way, different attacks can use different types of knockback / recoil
        //collision.gameObject.GetComponent<ResourceController>().Damage(damage, transform.position);
    }

    protected float GetDirection()
    {
        return Mathf.Abs(transform.localScale.x)/transform.localScale.x;
    }

}
