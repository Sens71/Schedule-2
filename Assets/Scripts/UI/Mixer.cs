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
public class SlotFilter
{
    public List<ReagentData> allowedItems = new();
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
    public List<SlotFilter> mainFilters = new();
    public List<ReagentData> sideBlackList = new();

    public SlotContent[] mainItems = new SlotContent[4];
    public SlotContent[] sideItems = new SlotContent[15];

    public List<LeafDrug> drugMap = new();
    public TimeManager timeManager;
    public Storage storage;
    public ClockTime cookDuration;

    public event Action OnChanged;

    private List<Drug> queue = new();
    private List<Drug> ready = new();
    private ClockTime portionReadyAt;

    public int QueueCount
    {
        get { return queue.Count; }
    }

    public List<Drug> Ready
    {
        get { return ready; }
    }

    private void Awake()
    {
        mainItems = Resize(mainItems, 4);
        sideItems = Resize(sideItems, 15);

        while (mainFilters.Count < 4)
            mainFilters.Add(new SlotFilter());

        HandleMixing();
    }

    public bool AcceptsMain(int index, ReagentData item)
    {
        List<ReagentData> allowed = mainFilters[index].allowedItems;
        return allowed.Count == 0 || allowed.Contains(item);
    }

    public bool AcceptsSide(ReagentData item)
    {
        return !sideBlackList.Contains(item);
    }

    public bool TryPlace(bool isMain, int index, ReagentData item)
    {
        SlotContent[] slots;
        if (isMain)
        {
            if (!AcceptsMain(index, item))
                return false;

            slots = mainItems;
        }
        else
        {
            if (!AcceptsSide(item))
                return false;

            slots = sideItems;
        }

        var content = slots[index];

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
        return true;
    }

    public bool TryTake(bool isMain, int index)
    {
        SlotContent[] slots;
        if (isMain)
        {
            slots = mainItems;
        }
        else
        {
            slots = sideItems;
        }
        var content = slots[index];
        if (content.IsEmpty)
            return false;

        content.item.ChangeAmount(1);
        RemoveOne(content);
        OnChanged?.Invoke();
        return true;
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
        foreach (var content in mainItems)
        {
            if (content.IsEmpty)
                return false;
        }
        return true;
    }

    private ItemData FindProduct()
    {
        foreach (var content in mainItems)
        {
            if (content.IsEmpty)
                continue;

            foreach (var pair in drugMap)
            {
                if (pair.leaf == content.item)
                    return pair.drug;
            }
        }
        return null;
    }

    private List<ReagentData> GetReagents()
    {
        List<ReagentData> reagents = new();

        foreach (var content in mainItems)
        {
            if (!content.IsEmpty)
                reagents.Add(content.item);
        }

        foreach (var content in sideItems)
        {
            if (!content.IsEmpty)
                reagents.Add(content.item);
        }

        return reagents;
    }

    private void ConsumePortion()
    {
        foreach (var content in mainItems)
        {
            if (!content.IsEmpty)
                RemoveOne(content);
        }

        foreach (var content in sideItems)
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

    private static SlotContent[] Resize(SlotContent[] slots, int size)
    {
        SlotContent[] result = new SlotContent[size];
        for (int i = 0; i < size; i++)
        {
            if (i < slots.Length)
            {
                result[i] = slots[i];
            }
            else
            {
                result[i] = new SlotContent();
            }
        }
        return result;
    }
}
