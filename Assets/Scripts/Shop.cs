using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public List<ExchangeToken> tokens = new();
    public GameObject itemSlot;
    private List<GameObject> itemSlots = new List<GameObject>();
    public Storage storage;
    void Start()
    {
        foreach (var token in tokens)
        {
            var slot = Instantiate(itemSlot, itemSlot.transform.parent);
            CreateSlot(token.itemRecieved, slot);
            itemSlots.Add(slot);
        }
        itemSlots.Remove(itemSlot);
        Destroy(itemSlot);
    }
    private void CreateSlot(ItemData itemData, GameObject slot)
    {
        var textAmount = slot.transform.Find("Amount").GetComponent<TMP_Text>();
        var textName = slot.transform.Find("Name").GetComponent<TMP_Text>();
        var icon = slot.transform.Find("Button").GetComponent<Image>();
        var bg = slot.transform.Find("Bg").GetComponent<Image>();
        textAmount.text = itemData.amount.ToString();
        textName.text = itemData.name;
        icon.sprite = itemData.icon;
        bg.color = itemData.bgColor;
    }
    public void Exchange(GameObject slot)
    {
        var index = itemSlots.IndexOf(slot);
        var amountGiven = tokens[index].amountGiven;
        var itemGiven = tokens[index].itemGiven;
        var amountRecieved = tokens[index].amountRecieved;
        var itemReceived = tokens[index].itemRecieved;
        itemGiven.ChangeAmount(-amountGiven);
        itemReceived.ChangeAmount(amountRecieved);
        CreateSlot(itemReceived, slot);
    }
}
[Serializable]
public class ExchangeToken
{
    public ItemData itemRecieved;
    public int amountRecieved;
    public ItemData itemGiven;
    public int amountGiven;
}
