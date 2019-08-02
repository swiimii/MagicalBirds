using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMultiSpawner : EnemySpawner
{
    // enemyPrefab player distanceUntilSpawn enemy fixedDirection directionIfFixed
    // all inherited from EnemySpawner

    // These lists must be the same size
    public List<Transform> spawnPositions;
    public List<GameObject> enemies;
    public bool repeats;
    private bool expended = false;

    private void Start()
    {
        if(!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        CreateEnemy();

    }

    protected override void Update()
    {
        
        if (player && Mathf.Abs(transform.position.x - player.transform.position.x) < distanceUntilSpawn)
        {

            for (int i = 0; i < enemies.Count; i++)
            {
                // Activate Enemy if player is within a distance threshold.
                if (enemies[i] && !enemies[i].activeInHierarchy)
                {
                    enemies[i].SetActive(true);
                }
                
            }

            if (!repeats)
            {
                gameObject.GetComponent<EnemyMultiSpawner>().enabled = false;
            }

        }

        else
        {
            for (int i = 0; i < spawnPositions.Count; i++)
            {
                if (!enemies[i])
                {
                    CreateEnemy(spawnPositions[i].position, i);
                }
            }

        }
        
    }



    public void CreateEnemy(Vector3 position, int index)
    {
        // Instantiate Enemy
        enemies[index] = Instantiate(enemyPrefab, spawnPositions[index]);
        enemies[index].transform.position = position;

        // Make the enemy face the direction of the player
        if(enemies[index].GetComponent<EnemyMovementController>())
            enemies[index].GetComponent<EnemyMovementController>().SetDirection(GetDirection());
        else
        {
            enemies[index].GetComponentInChildren<EnemyMovementController>().SetDirection(GetDirection());
        }

        //Only set active in update function
        enemies[index].SetActive(false);
    }

}
