using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "Quest", menuName = "MyData/Quests/Quest")]
public class Quest : ScriptableObject
{
    [SerializeField] private QuestData _questData;

    public void ChangeData(QuestData questData)
    {
        _questData.ApplyChangeFrom(questData);
    }

    public bool CompareData(QuestData questData)
    {
        return _questData.CompareTo(questData);
    }
}

