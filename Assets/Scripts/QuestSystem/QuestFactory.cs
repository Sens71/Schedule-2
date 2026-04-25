using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestFactory : MonoBehaviour
{
    [SerializeField] private ItemData moneyItem;
    [SerializeField] private QuestMarker[] markers;
    public List<Quest> activeQuests = new();
    public event Action<Quest> OnQuestCreated;
    public event Action<Quest> OnQuestCompleted;

    public Quest GenerateQuest(Order order)
    {
        var quest = new Quest();
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
        var freeMarkers = markers.Where(x=> x.Quest == null).ToArray();
        int randomIndex = Random.Range(0, freeMarkers.Length);
        freeMarkers[randomIndex].AssignQuest(quest);
        activeQuests.Add(quest);
        return quest;
    }
}
