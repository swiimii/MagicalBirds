using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatResourceController : EnemyResourceController
{
    public SpriteRenderer healthBar;
    public float healthBarSize, healthBarMaxSize;
    public CatArenaScript arena;
    

    public override void Start()
    {
        base.Start();
        healthBar.enabled = true;
        healthBarMaxSize = healthBar.size.x;
        healthBarSize = healthBarMaxSize;
    }

    public override void ProcessDamage(int damageDealt, Vector2 direction, float magnitude)
    {
        base.ProcessDamage(damageDealt, direction, magnitude);
        healthBar.size = new Vector2(healthBarMaxSize * currentHealth / maxHealth, healthBar.size.y);
    }

    public override void ProcessDamage(int damageDealt, Vector2 source)
    {        
        base.ProcessDamage(damageDealt, source);
        healthBar.size = new Vector2( Mathf.Clamp(healthBarMaxSize * currentHealth / maxHealth,0,100) , healthBar.size.y);
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
        var cat = GetComponent<CatEnemyScript>();
        var anim = GetComponent<Animator>();
        anim.SetBool("isRangedAttack", false);
        anim.SetBool("isMeleeAttack", false);
        anim.SetBool("isJumpAttack", false);

        cat.StopAllCoroutines();        
        cat.enabled = false;
        cat.PlaySound(cat.death);

        arena.StopBattle();

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
