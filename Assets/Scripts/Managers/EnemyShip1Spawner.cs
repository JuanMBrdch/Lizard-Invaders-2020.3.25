using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip1Spawner : MonoBehaviour
{
    [SerializeField] private GameObject ship;
    private Vector3 spawn;
    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 10f)
        {
            SpawnShip();
            timer = 0f;
        }
    }

    public void SpawnShip()
    {
        spawn = new Vector3(transform.position.x, Random.Range(transform.position.y - 4, transform.position.y + 4));
        Instantiate(ship, spawn, ship.transform.rotation);
    }
}
