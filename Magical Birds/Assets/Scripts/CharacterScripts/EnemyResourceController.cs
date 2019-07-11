using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyResourceController : ResourceController
{
    public override void Damage(int damageDealt)
    {
        base.Damage(damageDealt);
        GetComponent<Enemy>().immobile = true;

    }


}
