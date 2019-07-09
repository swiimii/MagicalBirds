// Script made by Samuel Scherer
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    // I am making this script for specific use with Foxly's 7-layer pixel art background
    // is made in tandem with BackgroundPieceState.cs
    public BackgroundPieceState background1; 
    public BackgroundPieceState background2;

    public GameObject trackedObject; // used for parallax math
    public Camera trackedCamera; // used for bounds
    public Vector2 prevTrackedPosition;

    // Set trackedcamera to main camera by default
    private void Start()
    {
        trackedCamera = Camera.main;
        prevTrackedPosition = trackedCamera.transform.position;
    }

    private void Update()
    {
        Vector2 newPos = trackedObject.transform.position;
        Vector2 difference = newPos - prevTrackedPosition;
        prevTrackedPosition = newPos;

        // move background1 according to tracked object movement
        var layers = background1.layers;
        var yCoeff = background1.parallaxChangeValueY;
        var xCoeff = background1.parallaxChangeValueX;
        var origins = background1.originPositions;
        var offsets = background1.offsets;

        // Move each layer for BG 1
        for (int i = 0; i < background1.layers.Length; i++)
        {
            // scroll layer
            layers[i].transform.position = origins[i] + offsets[i] + new Vector3(prevTrackedPosition.x * xCoeff[i], prevTrackedPosition.y * yCoeff[i]);

            if (difference.x > 0)
            {
                // move layer to the right if needed
                ReplaceRight(difference.x, background1, i);
            }
            else if (difference.x < 0)
            {
                // move layer to left if needed
                ReplaceLeft(difference.x, background1, i);
            }
        }

        // Now, interact only with BG 2
        layers = background2.layers;
        yCoeff = background2.parallaxChangeValueY;
        xCoeff = background2.parallaxChangeValueX;
        origins = background2.originPositions;
        offsets = background2.offsets;

        for (int i = 0; i < background2.layers.Length; i++)
        {
            layers[i].transform.position = origins[i] + offsets[i] + new Vector3(prevTrackedPosition.x * xCoeff[i], prevTrackedPosition.y * yCoeff[i]);
            if (difference.x > 0)
            {
                // move layer to the right if needed
                ReplaceRight(difference.x, background2, i);
            }
            else if (difference.x < 0)
            {
                // move layer to the left if needed
                ReplaceLeft(difference.x, background2, i);
            }
        }        
        
    }

    // Move the rightmost background to the left side of the player as they move left.
    private void ReplaceLeft(float difference, BackgroundPieceState background, int index)
    {
        var layer = background.layers[index];
        var sprite = layer.GetComponent<SpriteRenderer>();
        if( layer.transform.position.x - trackedObject.transform.position.x > sprite.bounds.size.x)
        {
            //background.originPositions[index] -= Vector3.right * sprite.bounds.size.x*2;
            background.offsets[index] -= Vector3.right * sprite.bounds.size.x * 2;
        }
    }

    // Move the leftmost background to the right side of the player as they move.
    private void ReplaceRight(float difference, BackgroundPieceState background, int index)
    {
        var layer = background.layers[index];
        var sprite = layer.GetComponent<SpriteRenderer>();
        if ( trackedObject.transform.position.x - layer.transform.position.x > sprite.bounds.size.x)
        {
            //background.originPositions[index] += Vector3.right * sprite.bounds.size.x*2;
            background.offsets[index] += Vector3.right * sprite.bounds.size.x * 2;

        }

    }

}
