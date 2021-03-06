﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChevronAttack : MeleeAttack
{
    public float knockbackMagnitude;
    protected Vector3 knockbackDirection = new Vector2(2, 1);

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        Vector2 realKnockbackDirection = new Vector2(knockbackDirection.x * transform.localScale.x/Mathf.Abs(transform.localScale.x), knockbackDirection.y);
        var resourceCtrlr = collision.gameObject.GetComponent<ResourceController>();
        if (resourceCtrlr)
        {
            resourceCtrlr.Damage(damage, realKnockbackDirection, knockbackMagnitude);
        }
    }
     
}
