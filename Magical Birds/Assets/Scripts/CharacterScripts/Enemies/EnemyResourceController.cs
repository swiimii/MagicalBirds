using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyResourceController : ResourceController
{
    public string enemyType = null;

    public override IEnumerator Death() {

        if(enemyType != null) {
            if(enemyType.ToLower() == "snake") {
                FindObjectOfType<Woodpecker>().SendMessage("incrementKilled");
            } else if(enemyType.ToLower() == "rat"){
                FindObjectOfType<Hummingbird>().SendMessage("incrementKilled");
            } else if(enemyType.ToLower() == "bat"){
                FindObjectOfType<Penguin>().SendMessage("incrementKilled");
            } else if(enemyType.ToLower() == "bosscat"){
                // TODO: do win?
            }
        }

        return base.Death();
    }

    public override void FixedDamageRecoil(Vector2 direction, float magnitude)
    {
        base.FixedDamageRecoil(direction, magnitude);
    }

    public override void DamageRecoil(Vector2 source)
    {
        base.DamageRecoil(source);
    }
}