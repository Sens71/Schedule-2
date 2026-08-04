using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class LeafDrug
{
    public ReagentData leaf;
    public ItemData drug;
}

public class Mixer : MonoBehaviour
{
    public List<MixerSlot> mainSlots = new();
    public List<MixerSlot> sideSlots = new();
    public List<LeafDrug> drugMap = new();
    public Storage storage;
    public Image resultIcon;
    public TMP_Text resultValueText;

    public event Action<Drug> OnMixed;
    
    void Start()
    {
        
    }
    public bool CanMix()
    {
        foreach(var slot in mainSlots)
        {
            if (slot.PlacedItem == null || slot.Count <= 0)
            {
                return false;
            }
                
        }
        return true;
    }

    public List<ReagentData> GetReagents(List<MixerSlot> slots)
    {
        List<ReagentData> reagents = new();
        foreach (var slot in slots)
        {
            if (slot.PlacedItem != null)
                reagents.Add(slot.PlacedItem);
        }

        return reagents;
    }
    public void Mix()
    {
        if (!CanMix())
            return;

        List<ReagentData> reagents = new();
        reagents.AddRange(GetReagents(mainSlots));
        reagents.AddRange(GetReagents(sideSlots));

        ItemData product = null;
        foreach (var slot in mainSlots)
        {
            var entry = drugMap.Find(pair => pair.leaf == slot.PlacedItem);
            if (entry == null)
                continue;
            product = entry.drug;
            break;
        }

        if (product == null)
        {
            Debug.LogError("Mixer: ни в одном основном слоте нет листа из drugMap", this);
            return;
        }

        var drug = new Drug(reagents.ToArray());
        drug.name = product.name;
        drug.icon = product.icon;
        storage.AddDrug(drug);

        foreach (var slot in mainSlots)
            slot.Consume();
        foreach (var slot in sideSlots)
            slot.Consume();

        ShowResult(drug);
        OnMixed?.Invoke(drug);
    }

    private void ShowResult(Drug drug)
    {
        if (resultIcon != null)
        {
            resultIcon.sprite = drug.icon;
            resultIcon.color = drug.iconColor;
        }

        if (resultValueText != null)
            resultValueText.text = Mathf.RoundToInt(drug.totalValue).ToString();
    }
}
