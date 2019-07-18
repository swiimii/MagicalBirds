using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBehavior : MonoBehaviour
{
    public GameObject BasicAttackPrefab, PowerAttackPrefab, RangedAttackPrefab;
    public Vector3 attackOffset;

    private readonly int extraJumps = 1;
    private int jumpsLeft;

    private void Start()
    {
        jumpsLeft = extraJumps;
        
    }

    public void ExtraJump()
    {
        jumpsLeft -= 1; // Remove a jump from "extraJumps". For double jumping, there should only be one available.
        GetComponent<PlayerMovementBehavior>().Jump(); 
    }

    public void BasicAttack()
    {
        var direction = Mathf.Abs(transform.localScale.x)/transform.localScale.x;
        var attack = Instantiate(BasicAttackPrefab, GetComponent<SpriteRenderer>().bounds.center + attackOffset * direction, Quaternion.identity);

        // Change directino of attack while maintaining scale
        attack.transform.localScale = 
        new Vector3(direction * attack.transform.localScale.x,
                    transform.localScale.y,
                    transform.localScale.z
                    );
    }

    public void SnowballAttack()
    {
        var direction = Mathf.Abs(transform.localScale.x) / transform.localScale.x;
        var attack = Instantiate(RangedAttackPrefab, GetComponent<SpriteRenderer>().bounds.center + attackOffset * direction, Quaternion.identity);
        var angle = attack.GetComponent<RangedAttack>().direction;
        attack.GetComponent<RangedAttack>().direction = new Vector2(angle.x * direction, angle.y);
        attack.transform.localScale =
        new Vector3(direction * attack.transform.localScale.x,
                    transform.localScale.y,
                    transform.localScale.z
                    );
    }

    public void ResetJumps()
    {
        if (jumpsLeft != extraJumps)
        {
            jumpsLeft = extraJumps;
        }
  
    }
    public bool HasJumpsRemaining()
    {
        return jumpsLeft > 0;
    }



}
