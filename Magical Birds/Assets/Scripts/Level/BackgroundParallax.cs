using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    // I am making this script for specific use with Foxly's 7-layer pixel art background
    // is made in tandem with BackgroundPieceController
    public BackgroundPieceController background1;
    public BackgroundPieceController background2;
    public float verticalScrollCoefficient;
    public float horizontalScrollCoefficient;

    // Only move the screen within a certain box area.
    // These can be set to empty gameobjects, which are placed throughout the level, called "right bounds" or etc.
    public Transform upperVerticalDeadzone; 
    public Transform lowerVerticalDeadzone;
    public Transform rightHorizontalDeadzone;
    public Transform leftHorizontalDeadzone;

    // Move the background left as the player moves right
    public void ScrollLeft()
    {

    }

    // Move the background right as the player moves left
    public void ScrollRight()
    {

    }

    // Move the background up as the player moves up
    public void ScrollUp()
    {

    }

    //move the background down as the player moves down
    public void ScrollDown()
    {

    }

}
