using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnInterval = 2f;
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
        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
        Vector3 spawnPos;
        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0:
                spawnPos = new Vector3(playerPos.x + Random.Range(-15f, 15f), 1.3f, playerPos.z + 20f);
                break;
            case 1:
                spawnPos = new Vector3(playerPos.x + Random.Range(-15f, 15f), 1.3f, playerPos.z - 20f);
                break;
            case 2:
                spawnPos = new Vector3(playerPos.x - 20f, 1.3f, playerPos.z + Random.Range(-15f, 15f));
                break;
            default:
                spawnPos = new Vector3(playerPos.x + 20f, 1.3f, playerPos.z + Random.Range(-15f, 15f));
                break;
        }

        GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Instantiate(randomEnemy, spawnPos, Quaternion.identity);
    }
}