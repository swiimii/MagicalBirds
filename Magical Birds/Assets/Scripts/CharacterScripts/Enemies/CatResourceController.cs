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
        // Cat is recoil-less
        return;
    }

    public override void DamageRecoil(Vector2 source)
    {
        // Cat has no recoil
        return;
    }
}
