using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LeafDrug
{
    public ReagentData leaf;
    public ItemData drug;
}



[Serializable]
public class SlotContent
{
    public ReagentData item;
    public int count;

    public bool IsEmpty
    {
        get { return item == null || count <= 0; }
    }
}

public class Mixer : MonoBehaviour
{
    public const int MainCount = 4;
    public const int SlotCount = 19;
    public const int LeafSlot = 3;

    public SlotContent[] slots = new SlotContent[SlotCount];

    public List<LeafDrug> drugMap = new();
    public TimeManager timeManager;
    public Storage storage;
    public ClockTime cookDuration;

    public event Action OnChanged;

    private List<Drug> queue = new();
    public List<Drug> ready = new();
    private ClockTime portionReadyAt;

    public int QueueCount
    {
        get { return queue.Count; }
    }

    private void Awake()
    {
        timeManager = FindAnyObjectByType<TimeManager>();
        slots = Resize(slots, SlotCount);

        HandleMixing();
    }


    public void Place(int index, ReagentData item)
    {
        SlotContent content = slots[index];

        if (content.IsEmpty)
        {
            content.item = item;
            content.count = 1;
        }
        else if (content.item == item)
        {
            content.count++;
        }
        else
        {
            content.item.ChangeAmount(content.count);
            content.item = item;
            content.count = 1;
        }

        item.ChangeAmount(-1);
        OnChanged?.Invoke();
    }


public void Take(int index)
    {
        var content = slots[index];
        if (content.IsEmpty)
            return;

        content.item.ChangeAmount(1);
        RemoveOne(content);
        OnChanged?.Invoke();
    }

    public void Mix()
    {
        if (!CanMix())
            return;

        ItemData product = FindProduct();
        if (product == null)
            return;

        Drug drug = new Drug(GetReagents().ToArray());
        drug.name = product.name;
        drug.icon = product.icon;

        if (queue.Count > 0 && !StaticsCalculations.CompareDrugs(drug, queue[0]))
            return;

        if (ready.Count > 0 && !StaticsCalculations.CompareDrugs(drug, ready[0]))
            return;

        ConsumePortion();
        queue.Add(drug);
    }

    public void CashResult()
    {
        foreach (var drug in ready)
            storage.AddDrug(drug);

        ready.Clear();
        OnChanged?.Invoke();
    }

    public ClockTime TimeLeft()
    {
        return portionReadyAt + cookDuration * (queue.Count - 1) - timeManager.GetCurrentTime();
    }

    private async void HandleMixing()
    {
        while (true)
        {
            await Awaitable.NextFrameAsync();

            if (queue.Count == 0)
                continue;

            portionReadyAt = timeManager.GetCurrentTime() + cookDuration;
            while (portionReadyAt > timeManager.GetCurrentTime())
                await Awaitable.NextFrameAsync();

            ready.Add(queue[0]);
            queue.RemoveAt(0);
            OnChanged?.Invoke();
        }
    }

    private bool CanMix()
    {
        for (int i = 0; i < MainCount; i++)
        {
            if(slots[i].IsEmpty)
                return false;
        }
        return true;
    }

    private ItemData FindProduct()
    {
        ReagentData leaf = slots[LeafSlot].item;
        foreach (LeafDrug pair in drugMap)
        {
            if (pair.leaf = leaf)
            {
                return pair.drug;
            }
        }
        return null;
    }

    private List<ReagentData> GetReagents()
    {
        List<ReagentData> reagents = new();

        foreach (var content in slots)
        {
            if (!content.IsEmpty)
                reagents.Add(content.item);
        }

        return reagents;
    }

    private void ConsumePortion()
    {

        foreach (var content in slots)
        {
            if (!content.IsEmpty)
                RemoveOne(content);
        }

        OnChanged?.Invoke();
    }

    private static void RemoveOne(SlotContent content)
    {
        content.count--;
        if (content.count <= 0)
        {
            content.item = null;
            content.count = 0;
        }
    }

    private static SlotContent[] Resize(SlotContent[] source,int size)
    {
        SlotContent[] result = new SlotContent[size];
        for (int i = 0; i < size; i++)
        {
            if (i < source.Length && source[i] != null)
            {
                result[i] = source[i];
            }
            else
            {
                result[i] = new SlotContent();
            }
        }
        return result;
    }
}
