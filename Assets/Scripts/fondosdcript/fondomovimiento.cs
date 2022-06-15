using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fondomovimiento : MonoBehaviour
{
    [SerializeField] private float velocidadfondo = 1f;
    [SerializeField] private float sizefondo = 1f;
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > (0))
            transform.position -= transform.right * velocidadfondo * Time.deltaTime;
        if (transform.position.x < 0)
        {
            transform.position = transform.right * sizefondo;
        }
    }
}
