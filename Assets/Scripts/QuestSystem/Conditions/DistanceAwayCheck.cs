using System;
using UnityEngine;


[Serializable]
public class DistanceAwayCheck : ConditionBase
{
    public Transform target1;
    public Transform target2;
    public float distance;
    public override bool CheckCondition()
    {
        if (Vector3.Distance(target1.position, target2.position) >= distance)
        {
            return true;
        }
        return false;
    }
}
