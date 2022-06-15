using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private Image healthbar;
    [SerializeField] private HealthManagerScript healthmanager;
    

    private void Awake()
    {
        healthmanager.OnHealthChanged += OnHealthChangedHandler;
        Updatehealth();
    }

    private void OnHealthChangedHandler(float currenthealth)
    {
        Updatehealth();
    }
    private void Updatehealth()
    {
        healthbar.fillAmount = healthmanager.GetCurrentHealthPercentage();
    }
}
