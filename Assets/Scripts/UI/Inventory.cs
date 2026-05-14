
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IBeginDragHandler
{
    public Storage storage;
    public GameObject itemSlot;
    private List<GameObject> itemSlots = new List<GameObject>();

    void Start()
    {
        foreach (var item in storage.items)
        {
            var slot = Instantiate(itemSlot, itemSlot.transform.parent);
            itemSlots.Add(slot);
        }
        itemSlots.Remove(itemSlot);
        Destroy(itemSlot);
        UpdateUI();
        
    }
    
    private void OnEnable()
    {
        foreach(ItemData item in storage.items)
        {
            item.OnChange += UpdateUI;
        }
    }
    private void OnDisable()
    {
        foreach (ItemData item in storage.items)
        {
            item.OnChange -= UpdateUI;
        }
    }
        
    private void CreateSlot(ItemData itemData, GameObject slot)
    {
        var textAmount = slot.transform.Find("Amount").GetComponent<TMP_Text>();
        var textName = slot.transform.Find("Name").GetComponent<TMP_Text>();
        var icon = slot.transform.Find("Button").GetComponent<Image>();
        var bg = slot.transform.Find("Bg").GetComponent<Image>();
        slot.GetComponent<Dragable>().item = itemData;
        textAmount.text = itemData.amount.ToString();
        textName.text = itemData.name.ToString();
        icon.sprite = itemData.icon;
        bg.color = itemData.bgColor;
    }
    
    private void UpdateUI()
    {
        var index = 0;
        foreach (ItemData item in storage.items)
        {
            CreateSlot(item, itemSlots[index]);
            index++;    
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("OnBeginDrag");
        print(eventData.position);
    }
}