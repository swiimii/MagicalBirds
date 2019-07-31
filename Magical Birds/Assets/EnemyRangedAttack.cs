using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : RangedAttack
{
    public Vector3 targetOffset = new Vector3(0, .2f,0);
    public override void Start()
    {
        base.Start();
        StartCoroutine("FollowPlayer");
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        //No Recoil
        var recoilTarget = collision.gameObject.GetComponent<ResourceController>();
        if (recoilTarget)
        {
            collision.gameObject.GetComponent<ResourceController>().Damage(damage, Vector2.zero, 0);
        }
        base.OnCollisionEnter2D(collision);
    }

    public IEnumerator FollowPlayer()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        while (gameObject)
        {
            // Move closer to the player at constant speed
            transform.position += (player.transform.position + targetOffset - transform.position) * speed * Time.deltaTime;
            yield return null;
        }
    }

}
