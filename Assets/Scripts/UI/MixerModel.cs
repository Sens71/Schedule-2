using System;
using System.Collections.Generic;
using UnityEngine;



public class MixerModel : MonoBehaviour
{
    public List<ReagentData> allowedMainItems = new();
    public List<ReagentData> allowedSideItems = new();

    public SlotContent[] mainItems = new SlotContent[4];
    public SlotContent[] sideItems = new SlotContent[15];

    public event Action OnChanged;

    private void Awake()
    {
        mainItems = Resize(mainItems, 4);
        sideItems = Resize(sideItems, 15);
    }

    public bool Accepts(bool isMain, ReagentData item)
    {
        var allowed = isMain ? allowedMainItems : allowedSideItems;
        return allowed.Count == 0 || allowed.Contains(item);
    }

    public bool TryPlace(bool isMain, int index, ReagentData item)
    {
        if (!Accepts(isMain, item))
            return false;

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

    public bool CanMix()
    {
        foreach (var content in mainItems)
        {
            if (content.IsEmpty)
                return false;
        }
        return true;
    }

    public List<ReagentData> GetReagents()
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

    public void ConsumePortion()
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
[Serializable]
public class SlotContent
{
    public ReagentData item;
    public int count;

    public bool IsEmpty
    {
        get {  return item == null || count <= 0; }
    }
}
