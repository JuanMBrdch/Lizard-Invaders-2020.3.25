using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frentemovimiento : MonoBehaviour
{
  
    [SerializeField] private float velocidadfrente = 8;
    [SerializeField] private float sizefondo = 1;
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > (0))
            transform.position -= transform.right * velocidadfrente * Time.deltaTime;
        if (transform.position.x < 0)
        {
            transform.position = transform.right * sizefondo;
        }
    }
}

