using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class TriggerCondition : ConditionBase
{
    [SerializeField] private Collider _trigger;
    
    public override bool CheckCondition()
    {
        var colliders = Physics.OverlapBox(_trigger.bounds.center, _trigger.bounds.extents, _trigger.transform.rotation);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Player player))
            {
                return true;
            }
        }
        return false;
    }
    


}