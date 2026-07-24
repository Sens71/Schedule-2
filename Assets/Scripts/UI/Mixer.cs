using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Mixer : MonoBehaviour
{
    public List<MixerSlot> mainSlots = new();
    public List<MixerSlot> sideSlots = new();
    public Storage storage;
    public Image resultIcon;
    public TMP_Text resultValueText;

    public event Action<CanDrug> OnMixed;
    
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

    public List<ReagentData> GetSideReagents()
    {   
        List<ReagentData> sideReagents = new();
        foreach (var slot in sideSlots)
        {
            if (slot.PlacedItem != null && slot.Count > 0)
            {
                sideReagents.Add(slot.PlacedItem);
            }
        }

        return sideReagents;
    }
    public void Mix()
    {   
        List<ReagentData> reagents = new();
        reagents.AddRange(GetSideReagents());
        reagents.AddRange(mainSlots.Select(a=>a.PlacedItem));
        var drug = new CanDrug(reagents.ToArray());
        storage.AddCanDrug(drug);
    }
}
