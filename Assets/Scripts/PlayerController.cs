using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 0.5f;
    public int ammoRewardPerKill = 3;
    public int maxDashCharges = 2;
    public float dashRechargeTime = 5f;
    public Transform innerTransform;

    private int currentDashCharges;
    private float dashRechargeTimer = 0f;
    private Rigidbody rb;
    public bool isDashing = false;
    private float dashCooldownTimer = 0f;
    public bool isMeleeMode = false;
    private PlayerShooting playerShooting;

    public TextMeshProUGUI dashText;
    public TextMeshProUGUI modeText;
    
    public GameObject gun;
    public GameObject axe;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerShooting = GetComponent<PlayerShooting>();
        currentDashCharges = maxDashCharges;
        UpdateDashUI();
        modeText.text = "Mode: Gun";
        axe.SetActive(false);
        gun.SetActive(true);
    }

    void Update()
    {
        dashCooldownTimer -= Time.deltaTime;

        if (currentDashCharges < maxDashCharges)
        {
            dashRechargeTimer += Time.deltaTime;
            if (dashRechargeTimer >= dashRechargeTime)
            {
                currentDashCharges++;
                dashRechargeTimer = 0f;
                UpdateDashUI();
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isMeleeMode = !isMeleeMode;
            modeText.text = isMeleeMode ? "Mode: Melee" : "Mode: Gun";
            gun.SetActive(!isMeleeMode);
            axe.SetActive(isMeleeMode);
        }

        if (Input.GetKeyDown(KeyCode.Q) && dashCooldownTimer <= 0f && !isDashing && currentDashCharges > 0)
        {
            StartCoroutine(Dash());
        }

        if (!isDashing)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            Vector3 moveDir = new Vector3(z, 0f, -x).normalized;
            rb.linearVelocity = new Vector3(moveDir.x * moveSpeed, rb.linearVelocity.y, moveDir.z * moveSpeed);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 lookDir = hit.point - transform.position;
            lookDir.y = 0f;
            if (lookDir != Vector3.zero)
                innerTransform.rotation = Quaternion.LookRotation(lookDir);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isDashing && isMeleeMode)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (!enemy.immuneToDash)
                {
                    enemy.TakeDamage(9999);
                    playerShooting.AddAmmo(ammoRewardPerKill);
                    currentDashCharges = Mathf.Min(currentDashCharges + 1, maxDashCharges);
                    UpdateDashUI();
                }
            }
        }
    }

    void UpdateDashUI()
    {
        dashText.text = "Dashes: " + currentDashCharges + " / " + maxDashCharges;
    }

    System.Collections.IEnumerator Dash()
    {
        isDashing = true;
        currentDashCharges--;
        UpdateDashUI();
        dashCooldownTimer = dashCooldown;
        rb.linearVelocity = innerTransform.forward * dashSpeed;
        yield return new WaitForSeconds(dashDuration + 0.1f);
        isDashing = false;
    }
}