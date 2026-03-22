using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;
    public float moveSpeed = 2f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameManager.instance.AddScore(10);
            Destroy(gameObject);
        }
    }
}