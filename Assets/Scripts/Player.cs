using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public QuestManager questManager;
    void Start()
    {
        questManager = FindAnyObjectByType<QuestManager>();
    }

    
    void Update()
    {
        
    }

    public void ReceiveQuest(Quest quest)
    {
        if (!questManager.quests.Contains(quest))
        questManager.quests.Add(quest);
            
    }
}
