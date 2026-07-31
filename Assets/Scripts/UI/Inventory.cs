using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IBeginDragHandler
{
    public Storage storage;
    public Dragable itemPrefab;
    public Transform contentParent;
    private List<Dragable> itemSlots = new List<Dragable>();

    void Start()
    {
        UpdateUI();
    }
    
    private void OnEnable()
    {
        foreach(ItemData item in storage.items)
        {
            item.OnChange += UpdateUI;
        }
        storage.OnChange += UpdateUI;
    }
    private void OnDisable()
    {
        foreach (ItemData item in storage.items)
        {
            item.OnChange -= UpdateUI;
        }
        storage.OnChange -= UpdateUI;
    }



    private void UpdateUI()
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("OnBeginDrag");
        print(eventData.position);
    }
}