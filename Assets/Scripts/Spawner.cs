using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    //maximum number of enemies per second
    [SerializeField] private float enemiesPerSecondCap = 10f;

    


    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    //enemies per second of the wave
    private float waveEnemiesPerSecond;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }
    private void Update()
    {
        if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;

        //time elapsed since the apperance of the enemy (e.g [1 / 0.1 == 10 sec)
        if (timeSinceLastSpawn >= (1f / waveEnemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;

        //Add health points for CASTLE health somwhere
        /*healthPoints--;*/
    }
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        waveEnemiesPerSecond = EnemiesPerSecond();
    }

    private void EndWave()
    {
        
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
       

    }

    private void SpawnEnemy()
    {
        //selecting enemy type to spawn at random
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[randomIndex];
        Instantiate(prefabToSpawn, Manager.main.startPoint.position,Quaternion.identity);
        Debug.Log("Enemy Spawned");
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseNumber * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    //function that returns the enemies per second of the current wave
    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor),0 , enemiesPerSecondCap);
    }
}
