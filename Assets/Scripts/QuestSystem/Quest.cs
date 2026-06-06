using System;

[Serializable]
public class Quest
{
    public event Action<Quest> OnQuestCompleted;
    public ItemData itemRequest;
    public ItemData itemReward;
    public int requiredAmount;
    public int rewardAmount;

    public void Complete()
    {
        OnQuestCompleted(this);
    }
}

public enum QuestState
{
    Ready,
    InProgress,
    Finished,
}

