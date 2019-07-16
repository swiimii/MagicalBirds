using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject player;
    public float distanceUntilSpawn;
    private GameObject enemy;

    public bool fixedDirection = false;
    public int directionIfFixed = 1; // Only useful if fixedDirection = true

    // Start is called before the first frame update
    private void Start()
    {
        if(!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        CreateEnemy();
    }
    private void Update()
    {
        if (enemy && enemy.activeInHierarchy)
        {
            return;
        }

        else if (enemy && !enemy.activeInHierarchy && Mathf.Abs(transform.position.x - player.transform.position.x) < distanceUntilSpawn)
        {
            enemy.SetActive(true);
        }

        else if (!enemy && Mathf.Abs(transform.position.x - player.transform.position.x) > distanceUntilSpawn)
        {
            CreateEnemy();
        }

    }
    
    public virtual void CreateEnemy()
    {
        // Instantiate Enemy
        enemy = Instantiate(enemyPrefab);
        enemy.transform.position = transform.position;

        // Make the enemy face the direction of the player
        enemy.GetComponent<EnemyMovementController>().SetDirection(GetDirection());

        //Only set active in update function
        enemy.SetActive(false);
    }

    public void DespawnEnemy()
    {
        enemy.SetActive(false);
        enemy.transform.position = transform.position;
        enemy.GetComponent<EnemyMovementController>().SetDirection(GetDirection());
    }

    public int GetDirection()
    {
        
        if(fixedDirection)
        {
            return directionIfFixed;
        }

        var retval = Mathf.Abs(enemy.transform.position.x - player.transform.position.x)
            / (enemy.transform.position.x - player.transform.position.x);
        return (int)retval;
    }

}
