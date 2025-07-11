using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "Quest", menuName = "MyData/Quests/Quest")]
public class Quest : ScriptableObject
{
    [SerializeField] private QuestData _questData;
    public QuestData QuestData => _questData;
    public event Action OnDataUpdate;

    public void ChangeData(QuestData questData)
    {
        _questData.ApplyChangesFrom(questData);
        OnDataUpdate?.Invoke();
    }

    public bool CompareData(QuestData questData)
    {
        return _questData.CompareTo(questData);
    }
}

public enum QuestState
{
    Ready,
    InProgress,
    Finished,
}

