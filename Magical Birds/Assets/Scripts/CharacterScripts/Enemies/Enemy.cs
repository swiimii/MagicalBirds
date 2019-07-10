using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health = 2, damageDealt = 1;
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
    public virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerResourcesController>().Damage(damageDealt, transform.position);
        }
    }
}
