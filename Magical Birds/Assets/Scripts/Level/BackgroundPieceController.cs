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

}
