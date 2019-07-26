using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementController : MonoBehaviour
{
    public abstract bool CheckGrounded();

    public abstract bool CheckBlocked();

}
