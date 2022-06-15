using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
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
            if (!healthController.IsAlive() && collision.gameObject.CompareTag("Enemy"))
            {
                if (P1)
                {
                    EnemyShip3Script enemyType3 = collision.GetComponent<EnemyShip3Script>();
                    if (enemyType3 != null)
                    {
                        GameManager.instance.AddPoints(enemyType3.pointValue, true);
                    }
                    EnemyShip1Script enemyType1 = collision.GetComponent<EnemyShip1Script>();
                    if (enemyType1 != null)
                    {
                        GameManager.instance.AddPoints(enemyType1.pointValue, true);
                    }
                }
                else
                {
                    EnemyShip3Script enemyType3 = collision.GetComponent<EnemyShip3Script>();
                    if (enemyType3 != null)
                    {
                        GameManager.instance.AddPoints(enemyType3.pointValue, false);
                    }
                    EnemyShip1Script enemyType1 = collision.GetComponent<EnemyShip1Script>();
                    if (enemyType1 != null)
                    {
                        GameManager.instance.AddPoints(enemyType1.pointValue, false);
                    }
                }
            }
        }
        Destroy(gameObject);
    }
}