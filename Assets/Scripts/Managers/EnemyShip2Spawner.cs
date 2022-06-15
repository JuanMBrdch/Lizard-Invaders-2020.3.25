using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip2Spawner : MonoBehaviour
{
    [SerializeField] private GameObject ship2;
    private Vector3 spawn;
    private float timer = 0f;  

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5f)
        {
            SpawnShip2();
            timer = 0f;
        }
    }

    public void SpawnShip2()
    {
        spawn = new Vector3(transform.position.x, Random.Range(transform.position.y - 4, transform.position.y + 4));
        Instantiate(ship2, spawn, ship2.transform.rotation);
    }
}
