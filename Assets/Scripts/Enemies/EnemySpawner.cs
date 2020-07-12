using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    //Configuration Parameters
    [SerializeField] Enemy[] enemyTypes = null;
    [SerializeField] Transform[] spawnPoints = null;
    [SerializeField] float minimumSpawnPause = 2f, maximumSpawnPause = 5f;
    [SerializeField] float minimumSpawnDelay = 0f, maximumSpawnDelay = 0.5f;
    [SerializeField] Transform enemyParent = null;

    //State Variables
    private Coroutine enemyWaveSpawner = null;
    private Enemy currentEnemyType = null;
    private int currentNumberToSpawn = 0;
    private float spawnTicker = 0;

    //Internal Methods
    private void Awake() {
        CheckEnemyTypeArray();
        CheckSpawnPointArray();
    }

    private void CheckEnemyTypeArray() {
        if (enemyTypes == null || enemyTypes.Length == 0) {
            Debug.LogWarning("No Enemies Specified In Enemy Spawner");
            enabled = false;
        }
    }

    private void CheckSpawnPointArray() {
        if (spawnPoints == null || spawnPoints.Length == 0) {
            Debug.LogWarning("No Spawn Points Specified In Enemy Spawner");
            enabled = false;
        }
    }

    private void Start() {
        SelectSpawnParametersForNextWave();
        spawnTicker = 5f;
    }

    private void SelectSpawnParametersForNextWave() {
        SelectRandomEnemyType();
        SelectRandomNumberToSpawn();
        SelectRandomDelay();
    }

    private void SelectRandomEnemyType() {
        int index = Random.Range(0, enemyTypes.Length);
        currentEnemyType = enemyTypes[index];
    }

    private void SelectRandomNumberToSpawn() {
        currentNumberToSpawn = Random.Range(1, spawnPoints.Length);
    }

    private void SelectRandomDelay() {
        spawnTicker = Random.Range(minimumSpawnPause, maximumSpawnPause) * Mathf.Sqrt(currentNumberToSpawn);
    }

    private void Update() {
        SpawnEnemies();
    }

    private void SpawnEnemies() {
        if (spawnTicker > 0f) {
            spawnTicker -= Time.deltaTime;
        } else {
            if (enemyWaveSpawner == null) {
                enemyWaveSpawner = StartCoroutine(SpawnCurrentWave());
                SelectSpawnParametersForNextWave();
            }
        }
    }

    private IEnumerator SpawnCurrentWave() {
        while (currentNumberToSpawn > 0) {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Enemy enemy = Instantiate(currentEnemyType, spawnPoint.position, Quaternion.identity);
            if (enemyParent) {
                enemy.transform.SetParent(enemyParent);
            }
            currentNumberToSpawn--;
            yield return new WaitForSecondsRealtime(Random.Range(minimumSpawnDelay, maximumSpawnDelay));
        }
        enemyWaveSpawner = null;
    }
}
