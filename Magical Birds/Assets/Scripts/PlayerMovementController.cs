using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float deadzoneValue = .1f;
    public bool moving;
    public bool grounded;
    public CapsuleCollider2D feetCollider;
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
        grounded = checkGrounded();

        //Simple jump function. Needs confirmation that the player is grounded
        if (Input.GetButtonDown("Jump") && grounded)
        {
            GetComponent<PlayerMovementBehavior>().Jump();
        }
    }

    private bool checkGrounded()
    {
        var col = feetCollider.bounds;
        float rayDistance = 0.02f;
        //Vector2 feet = new Vector2(col.center.x, col.min.y);//new Vector2(transform.position.x, GetComponent<Collider2D>().bounds.min.y); //(0,1) * bottom bounds  = (0,bottom bound)
        float radius = feetCollider.bounds.size.y / 2; //For a horizontal capsule, this is the radius
        float leftFeetBounds = feetCollider.bounds.min.x + radius, rightFeetBounds = feetCollider.bounds.max.x - radius; ; //radius is subtracted so that these rays are only fired from flat parts of collider
        var initialRayOrigin = new Vector2(leftFeetBounds, col.min.y);

        int numRays = 5; //max number of rays that are drawn
        for (int i = 0; i < numRays; i++)
        {
            float originOffset = i * (col.size.x - col.size.y) / (numRays - 1);

            var rayOrigin = initialRayOrigin + Vector2.right * originOffset;
            Debug.DrawRay(rayOrigin, Vector2.down * rayDistance, Color.green);
            var rayCast = Physics2D.Raycast(rayOrigin, Vector2.down, rayDistance, LayerMask.GetMask("Ground"));
            print(rayCast.transform);
            if (rayCast)
            {
                return true;
            }
            
        }
        return false;
    }
}
