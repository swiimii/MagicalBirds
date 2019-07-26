using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehavior : MovementBehavior
{
    public float speed = 1;
    public float jumpForce = 4;

    //NOTE: Recoil from damage is found in the PlayerResourcesController script

    public override void Move(float movementValue) //player walks left or right
    {
        // Movement offset, if the player can move
        float xmovement = Time.deltaTime * movementValue * speed;

        var cont = GetComponent<PlayerMovementController>();

        //flip the player's direction depending on direction of movement
        //also flips hitboxes
        if (movementValue < 0)
        {
            transform.localScale = new Vector2(-1, 1);
            if (!cont.CheckBlocked())
            {
                transform.position = new Vector2(transform.position.x + xmovement, transform.position.y);
            }
        }
        else if(movementValue > 0)
        {
            transform.localScale = new Vector2(1, 1);
            if (!cont.CheckBlocked())
            {
                transform.position = new Vector2(transform.position.x + xmovement, transform.position.y);
            }
        }
    }

    public void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
    }


}
