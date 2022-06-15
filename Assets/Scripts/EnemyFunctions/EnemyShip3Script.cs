using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip3Script : MonoBehaviour
{
    [SerializeField] private HealthManagerScript healthController;
    [SerializeField] private GameObject rayPrefab;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float rayFireTimer;
    [SerializeField] private float distance;
    [SerializeField] private float horizontalSpeed;
    private float currentRayFireTimer;
    private bool activateRay;
    private Rigidbody2D body;
    public int pointValue;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        healthController.ResetHealth();
    }



    void Update()
    {
        if (transform.position.x > 35.5)
        {
            transform.position -= transform.right * horizontalSpeed * Time.deltaTime;
        }
        if (transform.position.x <= 35.5f)
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
            if (!healthController.IsAlive())
            {
                Destroy(gameObject);
            }
        }



    }
    private void FireRay()
    {
        if (activateRay)
        {
            Instantiate(rayPrefab, shootPoint.position, shootPoint.rotation);
        }
    }
}
