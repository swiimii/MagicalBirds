using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovementController : MovementController
{
    public int damageDealt = 1;
    public float moveSpeed = 20;
    public abstract void Move();

    public virtual int GetDirection()
    {
        return (int)(transform.localScale.x / Mathf.Abs(transform.localScale.x));
    }
    public void SwitchDirection()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1,transform.localScale.y, transform.localScale.y);
    }
    public void SetDirection(int direction)
    {
        var scale = transform.localScale;
        transform.localScale = new Vector3(Mathf.Abs(scale.x) * direction, scale.y, scale.z);
    }
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        var erc = GetComponent<EnemyResourceController>();

        if (collision.gameObject.tag.Equals("Player")) 
        {
            if(erc && !erc.dead)
                collision.gameObject.GetComponent<PlayerResourcesController>().Damage(damageDealt, transform.position);
            else if(!erc)
                collision.gameObject.GetComponent<PlayerResourcesController>().Damage(damageDealt, transform.position);
        }
    }


}
