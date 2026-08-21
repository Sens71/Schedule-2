using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public TimeManager timeManager;
    public List<MixerSlot> mainSlots = new();
    public List<MixerSlot> sideSlots = new();
    public List<LeafDrug> drugMap = new();
    public Storage storage;
    public TMP_Text cookingTimerText;
    public ResultSlot result;
    public ClockTime cookDuration;
    public event Action<Drug> OnMixed;
    
    private bool ready;
    private List<ReagentData> currentReagents = new();
    private List<Drug> queue = new();

    private void Awake()
    {
        HandleMixing();
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
    public async void Mix()
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
        var drug = new Drug(reagents.ToArray());
        drug.name = product.name;
        drug.icon = product.icon;
        if (queue.Count > 0 && !StaticsCalculations.CompareDrugs(drug,queue[0]))
            return;
        
        foreach (var slot in mainSlots)
            slot.Consume();
        foreach (var slot in sideSlots)
            slot.Consume();
        queue.Add(drug);
    }

    private async void HandleMixing()
    {
        while (true)
        {
            await Awaitable.NextFrameAsync();
            if(queue.Count == 0)
                continue;
            var drug = queue[0];
            ClockTime nextReady = timeManager.GetCurrentTime() + cookDuration;
            ClockTime totalReady = timeManager.GetCurrentTime() + cookDuration * queue.Count;
            while (nextReady > timeManager.GetCurrentTime())
            {
                await Awaitable.NextFrameAsync();
                cookingTimerText.text = (totalReady - timeManager.GetCurrentTime()).ToString();
            }
            queue.RemoveAt(0);
            result.AddResult(drug);
        }
    }
    
}
