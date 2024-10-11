using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Spawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [Header("Attributes")]
    //number of enemies
    [SerializeField] private int baseNumber = 10;
    [SerializeField] private float enemiesPerSecond = 1f;
    [SerializeField] private float timeBetweenWaves = 15f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        StartWave();
    }
    private void Update()
    {
        if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;
        
        //time elapsed since the apperance of the enemy (e.g [1 / 0.1 == 10 sec)
        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;

        //Add health points for CASTLE health somwhere
        /*healthPoints--;*/
    }
    private void StartWave()
    {
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[0];
        Instantiate(prefabToSpawn, Manager.main.startPoint.position,Quaternion.identity);
        Debug.Log("Enemy Spawned");
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseNumber * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
}
