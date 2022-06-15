using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivationTrigger : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPatternsToActivate; 

    private void Awake()
    {
        for (int i = 0; i < enemyPatternsToActivate.Count ; i++)
        {
            enemyPatternsToActivate[i].SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < enemyPatternsToActivate.Count; i++)
            {
                enemyPatternsToActivate[i].SetActive(true);
            }
        }
    }
}
