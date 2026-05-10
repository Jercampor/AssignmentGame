using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float explosionRadius = 5f;
    public int explosionDamage = 999;
    public float fuseTime = 2f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fuseTime)
        {
            Explode();
        }
    }

    void Explode()
    {
        // Visual feedback - temporary explosion sphere
        GameObject explosion = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        explosion.transform.position = transform.position;
        explosion.transform.localScale = Vector3.one * explosionRadius * 2f;
        explosion.GetComponent<Renderer>().material.color = Color.red;
        Destroy(explosion.GetComponent<Collider>());
        Destroy(explosion, 0.2f);

        // Find all enemies in radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider col in colliders)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy != null && !enemy.immuneToDash)
                enemy.TakeDamage(explosionDamage);

            ShooterEnemy shooter = col.GetComponent<ShooterEnemy>();
            if (shooter != null)
                shooter.TakeDamage(explosionDamage);
        }

        Destroy(gameObject);
    }


    void OnCollisionEnter(Collision collision)
    {
        // Ignore player collision
        if (collision.gameObject.CompareTag("Player")) return;
        Explode();
    }
}