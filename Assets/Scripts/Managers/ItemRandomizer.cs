using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandomizer : MonoBehaviour
{
    public Transform pos;
    public GameObject[] Items;
    private Rigidbody2D body;
    bool velocidadstart = true;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (transform.position.y > 4)
        {
            body.velocity = Vector2.down;
        }
        else if (transform.position.y < -4)
        {
            body.velocity = Vector2.up;
            velocidadstart = false;
        }
        if (velocidadstart)
        {
            body.velocity = Vector2.down;
        }
    }
    void FixedUpdate()
    {
        if (Random.Range(0, 500) == 1 || Random.Range(0, 500) == 500)
        {
            IntantiateObject();
        }
    }

    private void IntantiateObject()
    {
        int n = Random.Range(0, Items.Length);
        Instantiate(Items[n], pos.position, Items[n].transform.rotation);
    }
}
