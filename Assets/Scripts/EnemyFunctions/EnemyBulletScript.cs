using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    [SerializeField] private float destroyTime;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    private float currentDestroyTime;
    public bool P1;

    // Update is called once per frame
    void Update()
    {
        CheckDestroy();
        transform.position += transform.right * speed * Time.deltaTime;
    }
    private void CheckDestroy()
    {
        currentDestroyTime += Time.deltaTime;
        if (currentDestroyTime >= destroyTime)
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
