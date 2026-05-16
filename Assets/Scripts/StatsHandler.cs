using System;
using UnityEngine;

public class StatsHandler : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float healAmount = 50;
    public event Action<GameObject> OnDeath;
    public event Action<float> OnHealthChanged;
    public ItemData med;
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
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth);
        if(currentHealth <= 0)
        {
            OnDeath?.Invoke(gameObject);
        }
        
    }



    private void Update()
    {
        
    }
}
