using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatResourceController : EnemyResourceController
{
    public GameObject healthBar;
    

    public override void Start()
    {
        base.Start();
        healthBar.SetActive(true);
    }

    public override void FixedDamageRecoil(Vector2 direction, float magnitude)
    {
        // Cat has no recoil
        return;
    }

    public override void DamageRecoil(Vector2 source)
    {
        // Cat has no recoil
        return;
    }
    public override IEnumerator Death()      
    {
        GetComponent<CatEnemyScript>().StopAllCoroutines();
        GetComponent<CatEnemyScript>().enabled = false;

        return base.Death();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var rc = collision.gameObject.GetComponent<ResourceController>();
        if (rc)
        {
            rc.Damage(1, transform.position);
        }
    }
}
