using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class Spawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] AudioSource newMissionCompleteMusic;  // Nowy utwór na zakończenie misji
    [SerializeField] AudioSource newMissionFailedMusic;  // Nowy utwór na niepowodzenie misji
    [SerializeField] AudioSource audioSource;  // Komponent AudioSource
    [SerializeField] AudioSource defaultMusic;  // Domyślna muzyka
    

    [Header("Attributes")]
    [SerializeField] public static int baseNumber = 2;
    [SerializeField] private float enemiesPerSecond = 1f;
    [SerializeField] private float timeBetweenWaves = 15f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float enemiesPerSecondCap = 10f;
    
    public static int currentLevel = 1;
    private int initialBaseHp = Goldholder.health;
    public static int baseHp;

    [Header("Wave Control")]
    [SerializeField] public static int totalWaves = 1; // liczba maksymalnych fal
    [SerializeField] private CanvasGroup uiCanvasGroup;
    [SerializeField] private GameObject[] gameObjectsGroup;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();
    [SerializeField] public Animator[] animators;

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    private float waveEnemiesPerSecond;
    private bool defeat = false;
    EnemyMovement enemyMovement;
    public static bool failedPanel = false;
    public static bool completePanel = false;


    private void Awake()
    {
        baseHp = initialBaseHp;
        onEnemyDestroy.AddListener(EnemyDestroyed);
        audioSource.Play();
    }

    private void Start()
    {
        StartCoroutine(StartWave());
        Debug.Log("Base HP:" + baseHp);
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

        if(baseHp <= 0)
        {
            baseHp++;
            defeat = true;
            MissionFailed();
            StartCoroutine(WaitForKeyPress());
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

        if(currentWave < totalWaves)
        {
            currentWave++;
            StartCoroutine(StartWave());
        }
        else if (currentWave >= totalWaves && !defeat)
        {
            MissionComplete();
            StartCoroutine(WaitForKeyPress());
        }
        
    }

    void LoadMainMenu()
    {
        // Ładowanie sceny "MainMenu"
        SceneManager.LoadScene("MainMenu");
    }

    private void MissionComplete()
    {
        if (animators == null)
        {
            Debug.LogError("Animator is not assigned!");
            return;
        }

        if (audioSource != null && newMissionCompleteMusic != null)
    {
        // Ustawienie nowego utworu
        audioSource.Stop();
        audioSource.PlayOneShot(newMissionCompleteMusic.clip);
    }
        Debug.Log("Mission Complete!");
        foreach (Animator animator in animators){
            animator.SetTrigger("isMissionCompleted");
        }
        BlockUserInterface();
        DisableInteraction();
    }

    private void MissionFailed()
{
    Debug.Log("MissionFailed called.");

    if (audioSource == null)
    {
        Debug.LogError("AudioSource is not assigned!");
        return;
    }

    if (newMissionFailedMusic == null)
    {
        Debug.LogError("newMissionFailedMusic is not assigned!");
        return;
    }
    // Ustawienie nowego utworu
    audioSource.Stop();
    audioSource.PlayOneShot(newMissionFailedMusic.clip);

    // Odpalamy nową muzykę
    Debug.Log("Mission Failed music is playing.");

    Debug.Log("Mission Failed!");
    foreach (Animator animator in animators)

    {
        animator.SetTrigger("isMissionFailed");
        Debug.Log("isMissionFailed triggered!");
    }
    BlockUserInterface();
    DisableInteraction();

    EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();
    if (enemies == null || enemies.Length == 0)
    {
        Debug.LogError("No EnemyMovement components found in the scene!");
    }
    else
    {
        foreach (EnemyMovement enemy in enemies)
        {
            enemy.UpdateSpeed(0f);
            enemiesLeftToSpawn = 0;
        }
    }
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
    private void BlockUserInterface()
{
    if (uiCanvasGroup != null)
    {
        uiCanvasGroup.interactable = false;
        uiCanvasGroup.blocksRaycasts = false;
        Debug.Log("UI interaction blocked.");
    }
    else
    {
        Debug.LogError("CanvasGroup is not assigned in the inspector!");
    }
}

    private void EnableInteraction()
{
    if (uiCanvasGroup != null)
    {
        uiCanvasGroup.interactable = true;
        uiCanvasGroup.blocksRaycasts = true;
        Debug.Log("UI interaction enabled.");
    }
    else
    {
        Debug.LogError("CanvasGroup is not assigned in the inspector!");
    }

    if (gameObjectsGroup != null)
    {
        // Iteruj przez wszystkie obiekty w grupie
        foreach (GameObject obj in gameObjectsGroup)
        {
            if (obj != null)
            {
            
                Collider2D collider = obj.GetComponent<Collider2D>();
                if (collider != null)
                {
                    collider.enabled = true; // włączenie Collider2D
                    Debug.Log("Collider enabled for object: " + obj.name);
                }
                else
                {
                    Debug.LogWarning("No Collider2D found on object: " + obj.name);
                }
            }
        }
    }
    else
    {
        Debug.LogError("gameObjectsGroup is not assigned!");
    }
}
    private void DisableInteraction()
{
    if (gameObjectsGroup != null)
    {
        // Iteruj przez wszystkie obiekty w grupie
        foreach (GameObject obj in gameObjectsGroup)
        {
            if (obj != null)
            {
                // Wyłączamy tylko Collider2D, aby obiekt nie reagował na kliknięcia, ale pozostaje widoczny
                Collider2D collider = obj.GetComponent<Collider2D>();
                if (collider != null)
                {
                    collider.enabled = false; // Wyłączenie Collider2D
                    Debug.Log("Collider disabled for object: " + obj.name);
                }
                else
                {
                    Debug.LogWarning("No Collider2D found on object: " + obj.name);
                }
            }
        }
    }
    else
    {
        Debug.LogError("gameObjectsGroup is not assigned!");
    }
}

IEnumerator WaitForKeyPress()
{
    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space));

    BuildingManager.main.SaveGame();

    if (Input.GetKeyDown(KeyCode.Escape))
    {
        LoadMainMenu();
    }
    else
    {
        LoadNextWave();
    }
}

private void LoadNextWave()
    {
        baseNumber += 2;
        currentWave = 1;
        totalWaves+=1;
        baseHp = initialBaseHp;
        currentLevel++;
        Debug.Log("Next Wave Loaded with increased difficulty");
        EnableInteraction();

        if (audioSource != null && newMissionCompleteMusic != null)
        {
            audioSource.Stop();
            audioSource.Play();
        }
        foreach (Animator animator in animators)
        {
            animator.ResetTrigger("isMissionCompleted");
        }
        StartCoroutine(StartWave());
    }
}



