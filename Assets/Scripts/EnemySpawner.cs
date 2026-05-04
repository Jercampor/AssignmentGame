using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject basicEnemy;
    public GameObject fastEnemy;
    public GameObject tankyEnemy;
    public GameObject shooterEnemy;
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

    GameObject GetRandomEnemy()
    {
        int roll = Random.Range(0, 10);
        if (roll < 4) return basicEnemy;       // 40% chance
        else if (roll < 7) return fastEnemy;   // 30% chance
        else if (roll < 9) return shooterEnemy; // 20% chance
        else return tankyEnemy;                // 10% chance
    }

    void SpawnEnemy()
    {
        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
        Vector3 spawnPos;
        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0:
                spawnPos = new Vector3(playerPos.x + Random.Range(-15f, 15f), 1f, playerPos.z + 20f);
                break;
            case 1:
                spawnPos = new Vector3(playerPos.x + Random.Range(-15f, 15f), 1f, playerPos.z - 20f);
                break;
            case 2:
                spawnPos = new Vector3(playerPos.x - 20f, 1f, playerPos.z + Random.Range(-15f, 15f));
                break;
            default:
                spawnPos = new Vector3(playerPos.x + 20f, 1f, playerPos.z + Random.Range(-15f, 15f));
                break;
        }

        Instantiate(GetRandomEnemy(), spawnPos, Quaternion.identity);
    }
}