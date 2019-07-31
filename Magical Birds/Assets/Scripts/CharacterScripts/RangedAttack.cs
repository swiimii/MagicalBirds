using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : Attack
{
    public float speed;
    public Vector2 direction;
    public float dissipationTime = .5f;

    public virtual void Start()
    {
        GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // Damage must be set in override. That way, different attacks can use different types of knockback / recoil
        //collision.gameObject.GetComponent<ResourceController>().Damage(damage, transform.position);
        
        EndAttack();
    }

    public override void EndAttack()
    {
        StartCoroutine("AttackDissipate");
    }
    
    public IEnumerator AttackDissipate()
    {
        GetComponent<Collider2D>().enabled = false;
        
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        
        if(GetComponent<Animator>())
        {
            GetComponent<Animator>().SetTrigger("finished");
        }

        yield return new WaitForSeconds(dissipationTime);
        Destroy(gameObject);
    }

}
