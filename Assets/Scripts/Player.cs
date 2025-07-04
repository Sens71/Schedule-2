using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private QuestManager questManager;
    void Start()
    {
        questManager = FindAnyObjectByType<QuestManager>();
    }

    public void ReceiveQuest(Quest quest)
    {
        questManager.AddQuest(quest);
    }
}
