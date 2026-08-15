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
    public Image resultIcon;
    public TMP_Text resultValueText;
    public TMP_Text cookingTimerText;

    public event Action<Drug> OnMixed;
    
    public ClockTime cookDuration;
    
    private bool ready;
    private List<ReagentData> currentReagents = new();
    public List<Drug> queuq = new();

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
        if (queuq.Count > 0 && !StaticsCalculations.CompareDrugs(drug,queuq[0]))
            return;
        
        foreach (var slot in mainSlots)
            slot.Consume();
        foreach (var slot in sideSlots)
            slot.Consume();
        queuq.Add(drug);
    }

    private async void HandleMixing()
    {
        while (true)
        {
            await Awaitable.NextFrameAsync();
            if(queuq.Count == 0)
                continue;
            var drug = queuq[0];
            ClockTime nextReady = timeManager.GetCurrentTime() + cookDuration;
            ClockTime totalReady = timeManager.GetCurrentTime() + cookDuration * queuq.Count;
            while (nextReady > timeManager.GetCurrentTime())
            {
                await Awaitable.NextFrameAsync();
                cookingTimerText.text = (totalReady - timeManager.GetCurrentTime()).ToString();
            }
            storage.AddDrug(drug);
            ShowResult(drug);
            OnMixed?.Invoke(drug);
        }
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
