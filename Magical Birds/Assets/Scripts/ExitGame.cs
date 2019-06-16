using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void doExitGame()
    {
        Debug.Log("has quit game");
        Application.Quit();
    }
}
