using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    [System.Serializable]
    public class Wave
    {
        public GameObject enemyPrefab;
        public float spawnTimer;
        public float spawnInterval;
        public int enemiesPerWave;
        public int spawnedEnemyCount;
    }

    public List<Wave> waves;
    public int waveNummber;
    public Transform minPos;
    public Transform maxPos;

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.Instance.gameObject.activeSelf == true) {
            waves[waveNummber].spawnTimer += Time.deltaTime;
            if(waves[waveNummber].spawnTimer >= waves[waveNummber].spawnInterval)
            {
                waves[waveNummber].spawnTimer = 0f;
                SpawnEnemy();
            }
            if(waves[waveNummber].spawnedEnemyCount >= waves[waveNummber].enemiesPerWave)
            {
                waves[waveNummber].spawnedEnemyCount = 0;
                if (waves[waveNummber].spawnInterval > 0.15f)
                {
                    waves[waveNummber].spawnInterval *= 0.8f; // decrease spawn interval by 20% for next wave
                }
                waveNummber++;
            }
            if(waveNummber >= waves.Count)
            {
                waveNummber = 0;
            }
        }
    }
    private void SpawnEnemy()
    {
        Instantiate(waves[waveNummber].enemyPrefab, RandomSpawnPoint(), transform.rotation);
        waves[waveNummber].spawnedEnemyCount++;
    }

    private Vector2 RandomSpawnPoint() {
        Vector2 spawnPoint;
        if (Random.Range(0f, 1f) > 0.5) {
            spawnPoint.x = Random.Range(minPos.position.x, maxPos.position.x);
            if (Random.Range(0f, 1f) > 0.5) {
                spawnPoint.y = minPos.position.y;
            } else {
                spawnPoint.y = maxPos.position.y;
            }
        } else {
            spawnPoint.y = Random.Range(minPos.position.y, maxPos.position.y);
            if (Random.Range(0f, 1f) > 0.5) {
                spawnPoint.x = minPos.position.x;
            } else {
                spawnPoint.x = maxPos.position.x;
            }
        }
        return spawnPoint; 
    }
}
