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
        GameObject explosion = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        explosion.transform.position = transform.position;
        explosion.transform.localScale = Vector3.one * explosionRadius * 2f;

        Material mat = explosion.GetComponent<Renderer>().material;
        mat.color = new Color(1f, 0.5f, 0f);
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", new Color(1f, 0.5f, 0f) * 3f);

        Destroy(explosion.GetComponent<Collider>());
        Destroy(explosion, 0.2f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider col in colliders)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy != null)
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