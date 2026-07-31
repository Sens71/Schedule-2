using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Storage", menuName = "MyData/Storage")]
public class Storage : ScriptableObject
{
    public ItemData[] items;
    [SerializeField]private List<Drug> drugs = new();

    /// <summary>Уникальные наркотики на складе. Один элемент = один стек.</summary>
    public IReadOnlyList<Drug> Drugs => drugs;

    /// <summary>Список наркотиков изменился — UI перерисовывается.</summary>
    public event Action OnChange;

    public void AddDrug(Drug drug)
    {
        if (TryGetCopy(drug, out var copy))
        {
            copy.amount++;
        }
        else
        {
            drugs.Add(drug);
            drug.amount++;
        }
        OnChange?.Invoke();
    }

    public void RemoveDrug(Drug drug)
    {
        if (TryGetCopy(drug, out var copy))
        {
            copy.amount--;
            if (copy.amount <= 0)
            {
                drugs.Remove(copy);
            }
            OnChange?.Invoke();
        }
    }

    private bool TryGetCopy(Drug drug, out Drug copy)
    {
        foreach (var existing in drugs)
        {
            if (existing.icon == drug.icon &&
                Mathf.Approximately(existing.adictivness, drug.adictivness) &&
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
