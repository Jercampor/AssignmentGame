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
    public int maxReserveAmmo = 30;
    private int reserveAmmo;
    public float reloadTime = 1.5f;
    private bool isReloading = false;

    public TextMeshProUGUI ammoText;
    private PlayerController playerController;

    void Start()
    {
        currentAmmo = maxAmmo;
        reserveAmmo = maxReserveAmmo;
        UpdateAmmoUI();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (isReloading) return;

        if (Input.GetMouseButtonDown(0) && !playerController.isMeleeMode)
        {
            if (currentAmmo > 0)
                Shoot();
            else
                ammoText.text = "No ammo! Press R to reload";
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            if (reserveAmmo > 0 && currentAmmo < maxAmmo)
                StartCoroutine(Reload());
            else if (reserveAmmo <= 0)
                ammoText.text = "No reserve ammo!";
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
        int ammoNeeded = maxAmmo - currentAmmo;
        int ammoToReload = Mathf.Min(ammoNeeded, reserveAmmo);
        currentAmmo += ammoToReload;
        reserveAmmo -= ammoToReload;
        isReloading = false;
        UpdateAmmoUI();
    }

    public void AddAmmo(int amount)
    {
        reserveAmmo = Mathf.Min(reserveAmmo + amount, maxReserveAmmo);
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        ammoText.text = "Ammo: " + currentAmmo + " / " + reserveAmmo;
    }
}