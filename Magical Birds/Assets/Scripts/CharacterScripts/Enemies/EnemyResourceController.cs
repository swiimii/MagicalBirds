using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyResourceController : ResourceController
{
    public string enemyType = null;

    private Woodpecker woodpecker;
    private Hummingbird hummingbird;
    private Penguin penguin;

    public override void Start()
    {
        base.Start();
        woodpecker = FindObjectOfType<Woodpecker>();
        hummingbird = FindObjectOfType<Hummingbird>();
        penguin = FindObjectOfType<Penguin>();

    }

    public override IEnumerator Death() {


        if(enemyType != null) {
            if(woodpecker && enemyType.ToLower() == "snake") {
                FindObjectOfType<Woodpecker>().SendMessage("incrementKilled");
            } else if(hummingbird && enemyType.ToLower() == "rat"){
                FindObjectOfType<Hummingbird>().SendMessage("incrementKilled");
            } else if(penguin && enemyType.ToLower() == "bat"){
                FindObjectOfType<Penguin>().SendMessage("incrementKilled");
            } else if(enemyType.ToLower() == "bosscat"){
                GameObject.FindGameObjectWithTag("Player").GetComponent<VictoryScreen>().Victory();
            }
        }

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>());

        foreach (Collider2D c in GetComponents<Collider2D>())
        {
            c.enabled = false;
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