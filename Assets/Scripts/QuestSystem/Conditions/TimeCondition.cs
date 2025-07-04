using System;
using UnityEngine;

public class TimeCondition : ConditionBase
{
    [Header("Start")]
    [SerializeField] private ClockTime _clockTimeStart;
    [Header("End")]
    [SerializeField] private ClockTime _clockTimeEnd;
    
    private TimeManager _timeManager;
    private void Awake()
    {
        _timeManager = FindAnyObjectByType<TimeManager>();
    }
    public override bool CheckCondition()
    {
        return _timeManager.WithinPeriod(_clockTimeStart, _clockTimeEnd);
    }
}

