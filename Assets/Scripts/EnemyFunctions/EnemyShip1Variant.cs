using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip1Variant : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootTime;
    private float currentShootTime;
    public float verticalspeed;

   

    void Update()
    {
        if (transform.position.y < -4.1)
        {
            verticalspeed = verticalspeed + verticalspeed * 2;
        }
        if (transform.position.y > 4.1)
        {
            verticalspeed = verticalspeed - verticalspeed * 2;
        }


        currentShootTime += Time.deltaTime;

        if (currentShootTime >= shootTime)
        {
            currentShootTime = 0;
            FireBullet();
        }
    }

    private void FireBullet()
    {
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
    }
}
