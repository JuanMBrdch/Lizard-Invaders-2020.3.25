using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShipScript : MonoBehaviour
{
    [SerializeField] private HealthManagerScript healthController;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject rayPrefab;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform bulletShootPoint;
    [SerializeField] private Transform bulletShootPoint2;
    [SerializeField] private Transform rayShootPoint;
    [SerializeField] private Vector2 move;
    [SerializeField] private float shootTime;
    [SerializeField] private float rayFireTimer;
    [SerializeField] private float distance;
    private float currentRayFireTimer;
    private float currentShootTime;
    private bool activateRay;
    public float verticalSpeed;
    private Rigidbody2D body;

    bool velocidadstart = true;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        healthController.ResetHealth();
        
    }


    void Update()
    {
        

        if (transform.position.y > 4)
        {
            body.velocity = Vector2.down;
        }
        else if (transform.position.y < -4)
        {
            body.velocity = Vector2.up;
            velocidadstart = false;
        }
        currentShootTime += Time.deltaTime;
        if (currentShootTime >= shootTime)
        {
            currentShootTime = 0;
            FireBullet();
        }

        if (healthController.currentHealth <= healthController.currentHealth / 2)
        {
            FireRay();
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, distance, playerLayer);
            if (hit)
            {
                activateRay = true;
                currentRayFireTimer += Time.deltaTime;
                if (currentRayFireTimer >= rayFireTimer)
                {
                    activateRay = false;
                    currentRayFireTimer = 0f;
                }
            }
            else
            {
                activateRay = false;
                currentRayFireTimer = 0f;
            }
        }

        else if (healthController.currentHealth <= healthController.currentHealth / 4)
        {
            shootTime = shootTime / 2;
            rayFireTimer = rayFireTimer * 2;
        }

        if (!healthController.IsAlive())
        {
            GameManager.instance.BossKill();
        }
        if (velocidadstart)
        {
            body.velocity = Vector2.down;
        }
    }


    private void FireBullet()
    {
        Instantiate(bulletPrefab, bulletShootPoint.position, bulletShootPoint.rotation);
        Instantiate(bulletPrefab, bulletShootPoint2.position, bulletShootPoint2.rotation);
    }

    

    private void FireRay()
    {
        if (activateRay)
        {
            Instantiate(rayPrefab, rayShootPoint.position, rayShootPoint.rotation);
        }
    }
}
