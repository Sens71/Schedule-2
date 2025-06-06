using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Quest> quests = new();
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void ReceiveQuest(Quest quest)
    {
        if (!quests.Contains(quest))
        quests.Add(quest);
            
    }
}
