using UnityEngine;

public class NPCController : MonoBehaviour
{
    private StatsHandler statsHandler;
    void Awake()
    {
        statsHandler = GetComponent<StatsHandler>();
    }
    private void OnEnable()
    {
        statsHandler.OnDeath += Death;
    }
    private void OnDisable()
    {
        statsHandler.OnDeath -= Death;
    }

    void Update()
    {
        
    }
    public void Death()
    {
        Destroy(gameObject);
    }
}
