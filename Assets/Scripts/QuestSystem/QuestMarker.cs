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
        
    }
}
