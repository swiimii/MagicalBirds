using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehavior : MonoBehaviour
{
    public float speed = 1;
    public float jumpForce = 4;
    public void Move(float movementValue) //player walks left or right
    {

        //move the player left or right depending on public speed value
        float xmovement = Time.deltaTime * movementValue * speed;
        transform.position = new Vector2(transform.position.x + xmovement, transform.position.y);

        //flip the player's direction depending on direction of movement
        //also flips hitboxes
        if (movementValue < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    public void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
    }

}
