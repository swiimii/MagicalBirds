﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage = 1;

    public virtual void EndAttack()
    {
        Destroy(gameObject);
    }
}
