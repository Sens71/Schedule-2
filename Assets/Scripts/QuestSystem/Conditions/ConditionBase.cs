using System;
using UnityEngine;
[Serializable]
public abstract class ConditionBase
{
    public abstract bool CheckCondition();

    public virtual void OnRemove()
    {
        
    }
}
