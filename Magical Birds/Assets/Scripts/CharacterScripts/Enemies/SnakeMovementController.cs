using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovementController : EnemyMovementController
{
    public BoxCollider2D feetCollider;
    public float immobileDuration = .7f;
    private bool moveReady = true;
    private void Update()
    {
        // move in opposite direction if blocked by a wall
        if(CheckBlocked())
        {
            SwitchDirection();
        }

        if (!immobile)
        {
            Move();
        }
    }

    public override void Move() 
    {
        if(moveReady)
        {
            StartCoroutine("Slink");
            //print("Moving");
            moveReady = false;
        }
    }

    // Snake-like movement: move in bursts, which quickly slow over time.
    IEnumerator Slink()
    {
        float currentSpeed = moveSpeed;
        while(currentSpeed > .8f)
        {
            transform.position += Time.deltaTime * currentSpeed * transform.right * GetDirection();
            //currentSpeed -= 1.5f;
            currentSpeed = Mathf.Lerp(currentSpeed, .05f, Time.deltaTime);
            yield return null;
        }
        moveReady = true;        
    }

    //Check if snake is running into a wall
    public override bool CheckBlocked()
    {
        
        var col = GetComponent<BoxCollider2D>().bounds;
        float rayDistance = .1f; // Distance ray will be fired

        int direction = GetDirection(); // +1 or -1
                                                        // print(direction);
        float offset = (col.size.x / 2 + .001f) * direction; // Width/2 of the collider * direction

        var rayOrigin = new Vector2(col.center.x + offset, col.center.y); // Origin is in front of the snake

        Debug.DrawRay(rayOrigin, transform.right * rayDistance * direction, Color.green); // Shows the direction of the ray in the editor
        var rayCast = Physics2D.Raycast(rayOrigin, transform.right * direction, rayDistance, LayerMask.GetMask("Ground")); // Cast ray in front of snake
        // print(rayCast.transform);

        if (rayCast)
        {
            return true; // Snake is blocked by a piece of terrain
        }
        return false; // Snake is unblocked
        
    }

    public override int GetDirection()
    {
        return (int)(transform.localScale.x / Mathf.Abs(transform.localScale.x) * -1);
    }

    public IEnumerator ImmobilizeUntilGrounded()
    {
        var playerCol = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), playerCol, true);
        // Stop the snake from moving
        StopCoroutine("Slink");
        moveReady = true;

        // Stop the snake from moving for a short period
        immobile = true;
        // Minimum immobile duration
        yield return new WaitForSeconds(immobileDuration);
        while (true)
        {
            if (CheckGrounded())
            {
                yield return null;

            }
            else
            {
                break;
            }
        }
        immobile = false;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), playerCol, false);

    }

    public override bool CheckGrounded()
    {
        var col = feetCollider.bounds;
        float rayDistance = 0.02f; // Distance ray will be fired
        float radius = feetCollider.bounds.size.y / 2; //For a horizontal capsule, this is the radius

        //radius is subtracted so that these rays are only fired from flat parts of the capsule collider
        float leftFeetBounds = feetCollider.bounds.min.x + radius, rightFeetBounds = feetCollider.bounds.max.x - radius;

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
