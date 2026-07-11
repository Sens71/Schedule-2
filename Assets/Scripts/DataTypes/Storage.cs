using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Storage", menuName = "MyData/Storage")]
public class Storage : ScriptableObject
{
    public ItemData[] items;
    [SerializeField]private List<CanDrug> canDrugs = new();

    public void AddCanDrug(CanDrug drug)
    {
        if (TryGetCopy(drug, out var copy))
        {
            copy.amount++;
        }
        else
        {
            canDrugs.Add(drug);
            drug.amount++;
        }
    }

    public void RemoveCanDrug(CanDrug drug)
    {
        if (TryGetCopy(drug, out var copy))
        {
            drug.amount--;
            if (drug.amount <= 0)
            {
                canDrugs.Remove(copy);
            }
        }
    }

    private bool TryGetCopy(CanDrug drug, out CanDrug copy)
    {
        foreach (var existing in canDrugs)
        {
            if (Mathf.Approximately(existing.adictivness, drug.adictivness) &&
                Mathf.Approximately(existing.energizing, drug.energizing) &&
                Mathf.Approximately(existing.focused, drug.focused) &&
                Mathf.Approximately(existing.athletics, drug.athletics) &&
                Mathf.Approximately(existing.calming, drug.calming) &&
                Mathf.Approximately(existing.brightEyed, drug.brightEyed) &&
                Mathf.Approximately(existing.disorienting, drug.disorienting) &&
                Mathf.Approximately(existing.foggy, drug.foggy) &&
                Mathf.Approximately(existing.glowing, drug.glowing) &&
                Mathf.Approximately(existing.longFaced, drug.longFaced) &&
                Mathf.Approximately(existing.sedating, drug.sedating) &&
                Mathf.Approximately(existing.seizure, drug.seizure) &&
                Mathf.Approximately(existing.slippery, drug.slippery) &&
                Mathf.Approximately(existing.sneaky, drug.sneaky))
            {
                copy = existing;
                return true;
            }
        }
        copy = null;
        return false;
    }
}
