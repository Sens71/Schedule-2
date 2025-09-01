using System;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<ExchangeToken> tokens = new();
    void Start()
    {
        
    }

    
    void Update()
    {
        
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
