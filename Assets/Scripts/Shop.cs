using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public List<ExchangeToken> buyTokens = new();
    public List<ExchangeToken> sellTokens = new();
    public GameObject itemSlot;
    private List<GameObject> buySlots = new List<GameObject>();
    private List<GameObject> sellSlots = new List<GameObject>();
    private bool isSelling;
    public GameObject panel;
    void Start()
    {
        Buy();
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
    public void Buy()
    {
        if(isSelling == false)
            return;
        isSelling = false;
        EraseSlots(sellSlots);
        GenerateSlots(buySlots, buyTokens);
    }
    public void Sell()
    {
        if (isSelling == true)
            return;
        isSelling = true;
        EraseSlots(buySlots);
        GenerateSlots(sellSlots, sellTokens);
    }
    private void GenerateSlots(List<GameObject> items, List<ExchangeToken> tokens)
    {
        itemSlot.SetActive(true);
        foreach (var token in tokens)
        {
            var slot = Instantiate(itemSlot, itemSlot.transform.parent);
            CreateSlot(token.itemRecieved, slot);
            items.Add(slot);
        }
        buySlots.Remove(itemSlot);
        itemSlot.SetActive(false);

    }
    private void EraseSlots(List<GameObject> items)
    {
        foreach (var item  in items)
        {
            if(item != null)
            {
                Destroy(item);
            }
        }
        items.Clear();
    }
    public void CloseShop()
    {
        panel.SetActive(false);
        var _actions = FindAnyObjectByType<Player>().inputActions;
        _actions.UI.Disable();
        _actions.PlayerControl.Enable();
    }
    public void Exchange(GameObject slot)
    {
        var index = 0;
        List<ExchangeToken> tokens;
        if (isSelling)
        {
            tokens = sellTokens;
            index = sellSlots.IndexOf(slot);    
        }
        else
        {
            tokens = buyTokens;
            index = buySlots.IndexOf(slot);
        }
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
