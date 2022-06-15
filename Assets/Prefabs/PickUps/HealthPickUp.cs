using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [SerializeField] private float healing;
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = -transform.right * speed * Time.deltaTime;
    }
    //Trigger que chequea que el jugador haya recolectado el pickup que le da la funcionalidad en cuestion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthManagerScript healthController = collision.GetComponent<HealthManagerScript>();
        if (healthController != null)
        {
            healthController.RecieveHeal(healing);            
        }
        Destroy(gameObject);
    }
}
