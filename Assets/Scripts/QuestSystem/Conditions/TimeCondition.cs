using System;
using UnityEngine;

public class TimeCondition : ConditionBase
{
    [Header("Start")]
    [SerializeField] private ClockTime _clockTimeStart;
    [Header("End")]
    [SerializeField] private ClockTime _clockTimeEnd;
    
    private TimeManager _timeManager;
    
    public override bool CheckCondition()
    {
        if(_timeManager == null)
            _timeManager = GameObject.FindAnyObjectByType<TimeManager>();
        return _timeManager.WithinPeriod(_clockTimeStart, _clockTimeEnd);
    }
}

