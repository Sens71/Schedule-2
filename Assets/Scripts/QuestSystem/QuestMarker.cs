using System;
using TMPro;
using UnityEngine;

public class QuestMarker : MonoBehaviour
{
    [SerializeField] private Storage playerStorage;
    [SerializeField] private float triggerDistance = 2f;
    [SerializeField] private MeshRenderer minimapMarker;
    [SerializeField] private TMP_Text questInfo;
    
    public Quest _quest;
    private Player _player;
    private MeshRenderer _meshRenderer;
    
    
    public Quest Quest => _quest;
    private void Awake()
    {
        _player = Player.Instance;
        _meshRenderer = GetComponent<MeshRenderer>();
        SetMarkerActive(false);
    }

    public void AssignQuest(Quest quest)
    {
        _quest = quest;
        questInfo.text = quest.itemRequest.name + ": " + quest.requiredAmount;
        print(gameObject.name);
    }

    private void Update()
    {
        if (_quest is null)
        {
            SetMarkerActive(false);
            return;
        }

        if (Vector3.Distance(transform.position, _player.transform.position) > triggerDistance)
        {
            SetMarkerActive(true);
            return;
        }
            
        TryComplete();
    }

    private void TryComplete()
    {
        if (_quest.requiredAmount > _quest.itemRequest.amount)
            return;
        
        _quest.itemReward.amount += _quest.rewardAmount;
        _quest.itemRequest.amount -= _quest.requiredAmount;
        _quest.Complete();
        _quest = null;
    }

    private void SetMarkerActive(bool active)
    {
        _meshRenderer.enabled = active;
        minimapMarker.enabled = active;
        questInfo.enabled = active;
    }
}
