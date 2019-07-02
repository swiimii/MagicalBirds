using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPieceController : MonoBehaviour
{
    // This script simply keeps track of a background GameObjects layers.
    // For use with BackgroundParallax.cs
    public GameObject[] layers;
    public float[] parallaxChangeValueX;
    public float[] parallaxChangeValueY;
    public Vector3[] originPositions; // Is reset in start function

    private void Start()
    {
        // Track the starting locations of each object. 
        originPositions = new Vector3[layers.Length];

        for(int i = 0; i < layers.Length; i++)
        {
            originPositions[i] = layers[i].transform.position; // Save the starting location of each layer to this array
        }
    }

}
