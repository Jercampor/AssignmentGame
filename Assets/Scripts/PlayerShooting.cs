/*using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    public Transform firePoint;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody>().linearVelocity = firePoint.forward * bulletSpeed;
            Destroy(bullet, 3f);
        }
    }
}*/


using UnityEngine;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    public Transform firePoint;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1.5f;
    private bool isReloading = false;

    public TextMeshProUGUI ammoText;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    void Update()
    {
        if (isReloading) return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        currentAmmo--;
        UpdateAmmoUI();
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().linearVelocity = firePoint.forward * bulletSpeed;
        Destroy(bullet, 3f);
    }

    System.Collections.IEnumerator Reload()
    {
        isReloading = true;
        ammoText.text = "Reloading...";
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
        UpdateAmmoUI();
    }

    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmo);
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        ammoText.text = "Ammo: " + currentAmmo + " / " + maxAmmo;
    }
}