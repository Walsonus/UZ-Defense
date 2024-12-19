using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private AudioClip newMissionCompleteMusic;  // Nowy utwór na zakończenie misji
    [SerializeField] AudioSource audioSource;  // Komponent AudioSource

    [Header("Attributes")]
    [SerializeField] private int baseNumber = 10;
    [SerializeField] private float enemiesPerSecond = 1f;
    [SerializeField] private float timeBetweenWaves = 15f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float enemiesPerSecondCap = 10f;

    [Header("Wave Control")]
    [SerializeField] private int totalWaves = 5; // liczba maksymalnych fal

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();
    public Animator animator;

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    private float waveEnemiesPerSecond;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
        audioSource.Play();
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

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

        if (currentWave >= totalWaves)
        {
            MissionComplete();
            Invoke("LoadMainMenu",10f);
        }
        else
        {
            currentWave++;
            StartCoroutine(StartWave());
        }
    }

    void LoadMainMenu()
    {
        // Ładowanie sceny "MainMenu"
        SceneManager.LoadScene("MainMenu");
    }

    private void MissionComplete()
    {
        if (animator == null)
        {
            Debug.LogError("Animator is not assigned!");
            return;
        }

        if (audioSource != null && newMissionCompleteMusic != null)
    {
        // Zatrzymanie obecnej muzyki przed zmianą
        audioSource.Stop();  

        // Ustawienie nowego utworu
        audioSource.clip = newMissionCompleteMusic;

        // Odpalamy nową muzykę
        audioSource.Play();  
    }
        Debug.Log("Mission Complete!");
        animator.SetTrigger("isMissionCompleted");
    }

    private void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[randomIndex];
        Instantiate(prefabToSpawn, Manager.main.startPoint.position, Quaternion.identity);
        Debug.Log("Enemy Spawned");
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseNumber * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0, enemiesPerSecondCap);
    }
}
