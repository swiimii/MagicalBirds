using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovementController : EnemyMovementController
{
    public Transform leftBound;
    public Transform rightBound;

    private void Update()
    {
        Move();
    }

    public override bool CheckBlocked()
    {
        // Unneeded for bat
        return false;
    }

    public override bool CheckGrounded()
    {
        return false;
    }

    public override void Move()
    {
        if(transform.position.x < leftBound.position.x)
        {
            SetDirection(1);
        }
        else if(transform.position.x > rightBound.position.x)
        {
            SetDirection(-1);            
        }

        transform.position = transform.position + moveSpeed * GetDirection() * Time.deltaTime * Vector3.right;
    }

}
