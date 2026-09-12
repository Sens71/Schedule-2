using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Storage storage;
    public Dragable itemPrefab;
    public Transform contentParent;
    private List<Dragable> itemSlots = new List<Dragable>();
    private bool needsRebuild;

    void Start()
    {
        Rebuild();
    }

    private void OnEnable()
    {
        foreach (ItemData item in storage.items)
        {
            item.OnChange += MarkDirty;
        }
        storage.OnChange += MarkDirty;
    }

    private void OnDisable()
    {
        foreach (ItemData item in storage.items)
        {
            item.OnChange -= MarkDirty;
        }
        storage.OnChange -= MarkDirty;
    }

    private void MarkDirty()
    {
        needsRebuild = true;
    }

    private void LateUpdate()
    {
        if (!needsRebuild)
            return;

        needsRebuild = false;
        Rebuild();
    }

    private void Rebuild()
    {
        for (int i = itemSlots.Count - 1; i >= 0; i--)
        {
            Destroy(itemSlots[i].gameObject);
        }
        itemSlots.Clear();
        foreach (ItemData item in storage.items)
        {
            if (item.amount <= 0) continue;
            var slot = Instantiate(itemPrefab, contentParent);
            slot.SetItem(item);
            itemSlots.Add(slot);
        }
        foreach (Drug drug in storage.Drugs)
        {
            if (drug.amount <= 0) continue;
            var slot = Instantiate(itemPrefab, contentParent);
            slot.SetDrug(drug);
            itemSlots.Add(slot);
        }
    }
}
