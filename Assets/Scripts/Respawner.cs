using System;
using UnityEngine;
using UnityEngine.AI;

public class Respawner : MonoBehaviour
{
    public NPCController[] npcPool;
    void Start()
    {
        
    }

    private void OnEnable()
    {
        foreach (NPCController npc in npcPool)
        {
            npc.GetComponent<StatsHandler>().OnDeath += DeactivateNpc;
        }
    }

    private void OnDisable()
    {
        foreach (NPCController npc in npcPool)
        {
            npc.GetComponent<StatsHandler>().OnDeath -= DeactivateNpc;
        }
    }
    

    private async void DeactivateNpc(GameObject obj)
    {
        var npc = obj.GetComponent<NPCController>();
        await Awaitable.WaitForSecondsAsync(7); 
        npc.gameObject.SetActive(false);
        await Awaitable.WaitForSecondsAsync(10);
        npc.gameObject.SetActive(true);
        var stats = npc.GetComponent<StatsHandler>();
        stats.currentHealth = stats.maxHealth;
        npc.GetComponent<NavMeshAgent>().enabled = true;
    }
    void Update()
    {
        
    }
}
