using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip1Script : MonoBehaviour
{
    [SerializeField] private HealthManagerScript healthController;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootTime;
    [SerializeField] public int pointValue;
    private float currentShootTime;
    public float verticalSpeed;
    public float horizontalSpeed;
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        healthController.ResetHealth();
    }

   

    void Update()
    {
        if (!healthController.IsAlive())
        {
            Destroy(gameObject);
        }
        //Condicion que hace a las naves entrar a la pantalla si quedan afuera de ella
        if (transform.position.x > 35.5)
        {
            transform.position -= transform.right * horizontalSpeed * Time.deltaTime;
        }
        //Condicion que asegura que las naves no disparen desde afuera de la pantalla de forma injusta
        if (transform.position.x <= 35.5f)
        {            
            currentShootTime += Time.deltaTime;

            if (currentShootTime >= shootTime)
            {
                currentShootTime = 0;
                FireBullet();
            }

            if (transform.position.y < -4.1)
            {
                transform.position += transform.up * verticalSpeed * Time.deltaTime;
            }
            else if (transform.position.y > 4.1)
            {
                transform.position -= transform.up * verticalSpeed * Time.deltaTime;
            }
        }
    }

    //Metodo para que las naves puedan disparar
    private void FireBullet()
    {
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
    }
    

}