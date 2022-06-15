using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip2Script : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    public bool P1;

    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
        if (transform.position.x <= 15f)
        {
            Destroy(gameObject);
        }   
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthManagerScript healthController = collision.GetComponent<HealthManagerScript>();
        if (healthController != null)
        {
            healthController.RecieveDamage(damage);
            if (!healthController.IsAlive() && collision.gameObject.CompareTag("Player"))
            {
                PlayerController player = collision.GetComponent<PlayerController>();
                player.Kill();
            }
        }
        Destroy(gameObject);
    }
}
