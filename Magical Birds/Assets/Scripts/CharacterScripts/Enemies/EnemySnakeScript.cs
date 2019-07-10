using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySnakeScript : Enemy
{
    private bool moveReady = true;
    private void Update()
    {
        // move in opposite direction if blocked by a wall
        if(CheckBlocked())
        {
            SwitchDirection();
        }

        Move();
    }

    public override void Move() 
    {
        if(moveReady)
        {
            StartCoroutine("Slink");
            print("Moving");
            moveReady = false;
        }
    }

    // Snake-like movement: move in bursts, which quickly slow over time.
    IEnumerator Slink()
    {
        print("Boi2");
        float currentSpeed = moveSpeed;
        print(GetDirection());
        while(currentSpeed > .8f)
        {
            transform.position += Time.deltaTime * currentSpeed * transform.right * GetDirection();
            //currentSpeed -= 1.5f;
            currentSpeed = Mathf.Lerp(currentSpeed, .05f, Time.deltaTime);
            print("boi");
            yield return null;
        }
        moveReady = true;        
    }

    //Check if snake is running into a wall
    private bool CheckBlocked()
    {
        
        var col = GetComponent<BoxCollider2D>().bounds;
        float rayDistance = .1f; // Distance ray will be fired

        int direction = GetDirection(); // +1 or -1
                                                        // print(direction);
        float offset = (col.size.x / 2 + .001f) * direction; // Width/2 of the collider * direction

        var rayOrigin = new Vector2(col.center.x + offset, col.center.y); // Origin is in front of the snake

        Debug.DrawRay(rayOrigin, transform.right * rayDistance * direction, Color.green); // Shows the direction of the ray in the editor
        var rayCast = Physics2D.Raycast(rayOrigin, transform.right * direction, rayDistance, LayerMask.GetMask("Ground")); // Cast ray in front of snake
        print(rayCast.transform);

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
}
