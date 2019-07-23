using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeResourceController : ResourceController
{

    public override void ProcessDamage(int damageDealt, Vector2 source)
    {
        base.ProcessDamage(damageDealt, source);
        StartCoroutine(GetComponent<SnakeMovementController>().ImmobilizeUntilGrounded());
    }

    public override void ProcessDamage(int damageDealt, Vector2 direction, float magnitude)
    {
        base.ProcessDamage(damageDealt, direction, magnitude);
        StartCoroutine(GetComponent<SnakeMovementController>().ImmobilizeUntilGrounded());
    }


}
