using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMovementController : EnemyMovementController
{
    private void Update()
    {
        // move in opposite direction if blocked by a wall
        if (CheckBlocked())
        {
            SwitchDirection();
        }

        Move();
    }

    public override void Move()
    {
        transform.position += moveSpeed * Time.deltaTime * Vector3.right * GetDirection();
    }

    public override int GetDirection()
    {
        return base.GetDirection() * -1;
    }

    public override bool CheckBlocked()
    {
        var col = GetComponent<SpriteRenderer>().bounds;
        float rayDistance = .25f; // Distance ray will be fired

        int direction = GetDirection(); // +1 or -1
                                        // print(direction);
                                        //float offset = (col.size.x / 2) * direction; // Width/2 of the collider * direction

        var rayOrigin = col.center;//new Vector2(col.center.x + offset, col.center.y + col.size.y / 2); // Origin is in front of the snake

        Debug.DrawRay(rayOrigin, transform.right * rayDistance * direction, Color.green); // Shows the direction of the ray in the editor
        var rayCast = Physics2D.Raycast(rayOrigin, transform.right * direction, rayDistance, LayerMask.GetMask("Ground")); // Cast ray in front of snake
        // print(rayCast.transform);

        if (rayCast)
        {
            return true; // Snake is blocked by a piece of terrain
        }
        return false; // Snake is unblocked

    }

    public override bool CheckGrounded()
    {
        var col = GetComponent<PolygonCollider2D>().bounds;
        float rayDistance = 0.02f; // Distance ray will be fired       

        //radius is subtracted so that these rays are only fired from flat parts of the capsule collider
        float leftFeetBounds = col.min.x, rightFeetBounds = col.max.x;

        var initialRayOrigin = new Vector2(leftFeetBounds, col.min.y);

        int numRays = 5; //max number of rays that are drawn
        for (int i = 0; i < numRays; i++)
        {
            float originOffset = i * (col.size.x - col.size.y) / (numRays - 1); // Offset the ray based on how many rays are being cast

            var rayOrigin = initialRayOrigin + Vector2.right * originOffset; // 
            Debug.DrawRay(rayOrigin, Vector2.down * rayDistance, Color.green); // Shows the direction of the ray in the editor
            var rayCast = Physics2D.Raycast(rayOrigin, Vector2.down, rayDistance, LayerMask.GetMask("Ground")); // Cast ray which checks if grounded
            //print(rayCast.transform);

            if (rayCast) // If the ray hits an object in the mask "Ground"
            {
                return true; // Player feet are "touching" the ground
            }
        }
        return false; // No ray hit the ground
    }
}
