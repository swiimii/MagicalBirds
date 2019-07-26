using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    public bool restrictedByBounds = false;
    public Transform bottomLeftBounds, topRightBounds;
    // Update is called once per frame
    private void Start()
    {
        if(!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    void Update()
    {
        if (player) // If player object isn't null, follow the player's position
        {
            var ptp = player.transform.position;
            transform.position = new Vector3(ptp.x, ptp.y) + offset;
        }
        if(restrictedByBounds)
        {
            var topBounds = topRightBounds.position.y;
            var rightBounds = topRightBounds.position.x;
            var bottomBounds = bottomLeftBounds.position.y;
            var leftBounds = bottomLeftBounds.position.x;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftBounds, rightBounds),
                                             Mathf.Clamp(transform.position.y, bottomBounds, topBounds),
                                             transform.position.z);
        }
    }
}
