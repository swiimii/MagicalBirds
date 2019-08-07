using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatArenaScript : MonoBehaviour
{

    public GameObject player, cat;
    public Camera gameCamera;
    public Transform arenaCamPosition;
    public BackgroundParallax background;
    public float distanceUntilActivate = 3;
    public VictoryScreen winScreen;

    private void Start()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (!gameCamera)
        {
            gameCamera = Camera.main;
        }

        StartCoroutine("CheckPlayerDistance");

    }

    public void StartBattle()
    {
        background.enabled = false;
        gameCamera.GetComponent<SimpleFollow>().enabled = false;
        gameCamera.transform.position = arenaCamPosition.position;
        cat.SetActive(true);
    }

    public void StopBattle()
    {
        gameCamera.GetComponent<SimpleFollow>().enabled = true;
        background.enabled = true;
        winScreen.StartCoroutine("Victory");
    }

    public IEnumerator CheckPlayerDistance()
    {
        while(Mathf.Abs(transform.position.x - player.transform.position.x) > distanceUntilActivate)
        {
            yield return null;
        }

        StartBattle();
    }

    
}
