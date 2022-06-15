using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthManagerScript : MonoBehaviour
{
    [HideInInspector] public float currentHealth;
    [SerializeField] private float maxHealth;
    public UnityEvent OnDie = new UnityEvent();
    public Action <float> OnHealthChanged;


    public float GetCurrentHealthPercentage()
    {
        return (float)currentHealth / maxHealth;
    }

    public void RecieveDamage(float damage)
    {
        currentHealth -= damage;
        OnHealthChanged?.Invoke(currentHealth);

        if (!IsAlive())
        {
            OnDie?.Invoke();
        }
    }
    public void RecieveHeal(float heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        OnHealthChanged?.Invoke(currentHealth);
    }
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public bool IsAlive()
    {
        return currentHealth > 0;
    }
}
