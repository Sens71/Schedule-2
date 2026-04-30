using System;
using UnityEngine;

public class QuestMarker : MonoBehaviour
{
    [SerializeField] private Storage playerStorage;
    [SerializeField] private float triggerDistance = 2f;
    
    private Quest _quest;
    private Player _player;
    
    public Quest Quest => _quest;
    private void Awake()
    {
        _player = Player.Instance;
    }

    public void AssignQuest(Quest quest)
    {
        _quest = quest;
        print(gameObject.name);
    }

    private void Update()
    {
        if(_quest == null)
            return;
        if(Vector3.Distance(transform.position, _player.transform.position) > triggerDistance) 
            return;
        TryComplete();
    }

    private void TryComplete()
    {
        print("Trying to complete quest");
        ItemData questItemRequired = null;
        ItemData questItemReward = null;
        foreach (var item in playerStorage.items)
        {
            if(_quest.itemRequest.name == item.name)
                questItemRequired = _quest.itemRequest;
        }
        foreach (var item in playerStorage.items)
        {
            if(_quest.itemReward.name == item.name)
                questItemReward = _quest.itemReward;
        }
        

        if (_quest.requiredAmount > questItemRequired.amount)
            return;
        questItemReward.amount += _quest.rewardAmount;
        questItemRequired.amount -= _quest.requiredAmount;
        print("quest Completed");
        _quest = null;
    }
}
