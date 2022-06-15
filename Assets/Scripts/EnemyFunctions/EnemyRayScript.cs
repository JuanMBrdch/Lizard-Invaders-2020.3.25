using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRayScript : MonoBehaviour
{
    [SerializeField] private float destroyTime;
    [SerializeField] private float damage;
    private float currentDestroyTime;
    public bool P1;

    
    void Update()
    {
        CheckDestroy();
    }
    private void CheckDestroy()
    {
        currentDestroyTime += Time.deltaTime;
        if (currentDestroyTime >= destroyTime)
        {
            currentDestroyTime = 0f;
            Destroy(gameObject);
        }
    }
    //Trigger que se encarga de quitarle vida al jugador una vez el rayo colisiona con él
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
