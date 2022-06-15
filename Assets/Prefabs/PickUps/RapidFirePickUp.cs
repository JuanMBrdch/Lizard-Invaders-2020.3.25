using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFirePickUp : MonoBehaviour
{     
    [SerializeField] private float speed;
    [SerializeField] private float rfTimer;
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
        PlayerController playerController = collision.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.FireRateRateUp(rfTimer);
            Destroy(gameObject);
        }
    }
}
