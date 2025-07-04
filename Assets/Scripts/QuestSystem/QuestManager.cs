using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public HashSet<Quest> quests = new();
    public List<GameObject> questPanels = new();
    public void AddQuest(Quest quest)
    {
        quests.Add(quest);
        UpdateUI();
    }

    private void UpdateUI()
    {
        
    }
}
