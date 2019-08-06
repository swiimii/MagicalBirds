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
    public virtual void Start()
    {
        if(!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        CreateEnemy();
    }

    protected virtual void Update()
    {
        if (player)
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

    }
    
    public virtual void CreateEnemy(Vector3 position)
    {
        // Instantiate Enemy
        enemy = Instantiate(enemyPrefab);
        enemy.transform.position = position;

        // Make the enemy face the direction of the player
        if(enemy.GetComponent<EnemyMovementController>())
            enemy.GetComponent<EnemyMovementController>().SetDirection(GetDirection());
        else
        {
            enemy.GetComponentInChildren<EnemyMovementController>().SetDirection(GetDirection());
        }

        //Only set active in update function
        enemy.SetActive(false);
    }

    public virtual void CreateEnemy()
    {
        CreateEnemy(transform.position);
    }

    public virtual void DespawnEnemy(Vector3 position)
    {
        enemy.SetActive(false);
        enemy.transform.position = position;
    }

    public virtual void DespawnEnemy()
    {
        DespawnEnemy(transform.position);
    }

    public virtual int GetDirection()
    {
        
        if(fixedDirection)
        {
            return directionIfFixed;
        }

        print(enemy);
        print(player);
        var retval = Mathf.Abs(enemy.transform.position.x - player.transform.position.x)
            / (enemy.transform.position.x - player.transform.position.x);
        return (int)retval;
    }

}
