using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Wave 
{
    public int totalSpawnEnemies;
    public int numberOfRandomSpawnPoint;
    public float delayStart;
    public float spawnInterval;
    public int numberOfPowerUp;
}

public class WaveSpawner : MonoBehaviour
{
    [Header("Wave Configuration")]
    public Wave[] waves;
    public Transform[] allSpawnPoints;
    public GameObject enemyPrefab;
    public GameObject[] powerUpPrefabs;
    private List<Transform> currentSelectedPoints = new List<Transform>();

    void Start()
    {
        StartCoroutine(SpawnWavesRoutine());
    }

    IEnumerator SpawnWavesRoutine()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            Wave currentWave = waves[i];
            GetRandomSpawnPoints(currentWave.numberOfRandomSpawnPoint);
            SpawnPowerUps(currentWave.numberOfPowerUp);
            yield return new WaitForSeconds(currentWave.delayStart);
            
            for (int j = 0; j < currentWave.totalSpawnEnemies; j++)
            {
                Transform randomPoint = currentSelectedPoints[Random.Range(0, currentSelectedPoints.Count)];
                Instantiate(enemyPrefab, randomPoint.position, randomPoint.rotation);
                yield return new WaitForSeconds(currentWave.spawnInterval);
            }
            while (FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length > 0)
            {
                yield return null; 
            }
            yield return new WaitForSeconds(2.0f);
        }
    }
    
    void SpawnPowerUps(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float randomX = Random.Range(-8, 8);
            float randomZ = Random.Range(-8, 8);
            
            Vector3 randomPos = new Vector3(randomX, 0.5f, randomZ); 
            
            int randomIndex = Random.Range(0, powerUpPrefabs.Length);
            GameObject selectedPowerUp = powerUpPrefabs[randomIndex];
            Instantiate(selectedPowerUp, randomPos, Quaternion.identity);
        }
    }
    
    void GetRandomSpawnPoints(int count)
    {
        currentSelectedPoints.Clear(); 
        List<Transform> available = new List<Transform>(allSpawnPoints);
        count = Mathf.Min(count, available.Count); 
        
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, available.Count);
            currentSelectedPoints.Add(available[randomIndex]); 
            available.RemoveAt(randomIndex); 
        }
        
    }
}