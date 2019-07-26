using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrabBehavior : EnemyMovementController
{
    public override void Move()
    {

    }
    public override bool CheckBlocked()
    {
        return false;
    }
    public override bool CheckGrounded()
    {
        return true;
    }
}
