using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<ResourceController>().Damage(1, Vector2.up, 3);
    }
}
