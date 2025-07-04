using UnityEngine;

public class TriggerCondition : ConditionBase
{
    private bool _withinZone;
    public override bool CheckCondition()
    {
        return _withinZone;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _withinZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _withinZone = false;
        }
    }
}