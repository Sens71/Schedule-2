using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "Quest", menuName = "MyData/Quests/Quest")]
public class Quest : ScriptableObject
{
    public QuestStep[] steps;
    public int currentQuestProgress;
    public QuestState questState = QuestState.Ready;

    public void MoveToPlace(QuestData questData)
    {
        
    }

    public void TalkToPerson(QuestData questData)
    {
        
    }
}

[Serializable]
public class QuestStep
{
    public UnityEvent stepAction;
    public string stepDescription;
    public string stepName;
}

public enum QuestState
{
    Ready,
    NotReady,
    Taken,
    InProgress,
    Completed,
}
