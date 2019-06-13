using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        var ptp = player.transform.position;
        transform.position = new Vector3(ptp.x, ptp.y, transform.position.z);
    }
}
