// Much of this code was created by Samuel Scherer
// Other contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MovementController
{
    public float deadzoneValue = .1f;
    public bool moving, grounded, blocked;

    public CapsuleCollider2D feetCollider, frontCollider;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if the horizontal axis is being pressed, move the player horizontally
        float movementValue = Input.GetAxisRaw("Horizontal");

        if ( Mathf.Abs(movementValue) > deadzoneValue) 
        {
            moving = true;
            GetComponent<Animator>().SetBool("IsMoving", true); //tell animation controller that the player is moving
            //move the player
            GetComponent<PlayerMovementBehavior>().Move(movementValue);
        }
        else if(moving) //set animator moving bool and player moving bool to false
        {
            GetComponent<Animator>().SetBool("IsMoving", false);
            moving = false;
        }

        //check if player is on the ground
        grounded = CheckGrounded();
        GetComponent<Animator>().SetBool("IsGrounded", grounded); // Tell the animator that the player's not grounded


        //Simple jump function. Needs confirmation that the player is grounded
        // Double jumps are controlled in the basic ability controller
        if (Input.GetButtonDown("Jump") && grounded)
        {
            GetComponent<PlayerMovementBehavior>().Jump();
        }
    }

    // Check if the player is touching an object on the "ground" layer
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


    // Used for checking if the player is running into a wall. 
    // Called from PlayerMovementBehavior class
    public override bool CheckBlocked()
    {
        var col = frontCollider.bounds;
        float rayDistance = .02f; // Distance ray will be fired

        int direction = (int)transform.localScale.x; // +1 or -1
        // print(direction);
        float offset = col.size.x/2 * direction; // Width/2 of the collider * direction

        var rayOrigin = new Vector2(col.center.x + offset, col.center.y); // Origin is in front of the player

        Debug.DrawRay(rayOrigin, transform.right * rayDistance * direction, Color.blue); // Shows the direction of the ray in the editor
        var rayCast = Physics2D.Raycast(rayOrigin, transform.right * direction, rayDistance, LayerMask.GetMask("Ground")); // Cast ray in front of user
        // print(rayCast.transform);

        if (rayCast)
        {
            return true; // Player is blocked by a piece of terrain
        }
        return false; // Player is unblocked
    }
    // code for moving platform used in Fall level to stick myrd to platform - JVN
    void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.tag == "moving_platform")
            transform.parent = other.transform;
    }
    // code for moving platform used in Fall level to UNstick myrd to platform - JVN
    void OnCollisionExit2D(Collision2D other) {
        if (other.transform.tag == "moving_platform")
            transform.parent = null;
    }
}
