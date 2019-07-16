using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    public GameObject player;
    public int cameraOffset;
    // Update is called once per frame
    void Update()
    {
        if (player) // If player object isn't null, follow the player's position
        {
            var ptp = player.transform.position;
            transform.position = new Vector3(ptp.x, ptp.y, cameraOffset);
        }
    }
}
