using System;


public class Quest
{
    public ItemData itemRequest;
    public ItemData itemReward;
    public int requiredAmount;
    public int rewardAmount;
}

public enum QuestState
{
    Ready,
    InProgress,
    Finished,
}

