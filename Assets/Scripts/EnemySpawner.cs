using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float mapWidth = 25f;
    public float mapHeight = 25f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        int side = Random.Range(0, 4);
        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
        Vector3 spawnPos;

        switch (side)
        {
            case 0: // Top
                spawnPos = new Vector3(playerPos.x + Random.Range(-15f, 15f), 0f, playerPos.z + 20f);
                break;
            case 1: // Bottom
                spawnPos = new Vector3(playerPos.x + Random.Range(-15f, 15f), 0f, playerPos.z - 20f);
                break;
            case 2: // Left
                spawnPos = new Vector3(playerPos.x - 20f, 0f, playerPos.z + Random.Range(-15f, 15f));
                break;
            default: // Right
                spawnPos = new Vector3(playerPos.x + 20f, 0f, playerPos.z + Random.Range(-15f, 15f));
                break;
        }

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}