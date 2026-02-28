using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageGenerator: MonoBehaviour
{
    [SerializeField] private OrderFactory orderFactory;
    private List<MessagePreview> messages = new();
    [SerializeField] private MessagePreview messagePreviewPrefab;
    [SerializeField] private Transform parent;

    private void OnEnable()
    {
       orderFactory.OnOrderCreated += CreateMessage;
       orderFactory.OnOrderExpired += DeleteMessage;
    }

    private void OnDisable()
    {
        orderFactory.OnOrderCreated -= CreateMessage;
        orderFactory.OnOrderExpired -= DeleteMessage;
    }

    private void CreateMessage(Order order)
    {
        var message = Instantiate(messagePreviewPrefab, parent);
        message.SetOrder(order);
    }

    private void DeleteMessage(Order order)
    {
        
    }
}