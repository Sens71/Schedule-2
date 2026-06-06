using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

public class QuestFactory : MonoBehaviour
{
    [SerializeField] private ItemData moneyItem;
    [SerializeField] private QuestMarker[] markers;
    public List<Quest> activeQuests = new();

    public Quest GenerateQuest(Order order)
    {
        var quest = new Quest();
        quest.OnQuestCompleted += QuestCompleted;
        quest.itemRequest = order.ItemData;
        quest.requiredAmount = order.Amount;
        quest.itemReward = moneyItem;
        if (order.OfferedPrice == 0)
        {
            quest.rewardAmount = order.Price;
        }
        else
        {
            quest.rewardAmount = order.OfferedPrice;
        }
        
        var freeMarkers = markers.Where((marker) => marker.Quest == null).ToArray();
        if (freeMarkers.Length == 0)
        {
            Debug.LogError("No free markers found");
        }
        int randomIndex = Random.Range(0, freeMarkers.Length);
        freeMarkers[randomIndex].AssignQuest(quest);
        activeQuests.Add(quest);
        return quest;
    }


    private void QuestCompleted(Quest quest)
    {
        quest.OnQuestCompleted -= QuestCompleted;
        activeQuests.Remove(quest);
    }
}
