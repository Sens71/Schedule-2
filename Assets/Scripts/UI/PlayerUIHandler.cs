using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHandler : MonoBehaviour
{
    private Player player;
    private StatsHandler statsHandler;
    public Slider slider;
    void Awake()
    {
        player = FindAnyObjectByType<Player>();
        statsHandler = player.GetComponent<StatsHandler>();
        UpdateHealth(statsHandler.maxHealth);
    }
    private void OnEnable()
    {
        statsHandler.OnHealthChanged += UpdateHealth;
    }
    private void OnDisable()
    {
        statsHandler.OnHealthChanged -= UpdateHealth;
    }
    private void UpdateHealth(float currentHealth)
    {
        slider.value = currentHealth / statsHandler.maxHealth;
    }
}
