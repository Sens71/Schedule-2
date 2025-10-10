using System;
using UnityEngine;

public class StatsHandler : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public event Action OnDeath;
    public event Action<float> OnHealthChanged;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(float change)
    {
        currentHealth += change;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            OnDeath?.Invoke();
        }
        OnHealthChanged?.Invoke(currentHealth);
    }
    void Update()
    {
        
    }
}
