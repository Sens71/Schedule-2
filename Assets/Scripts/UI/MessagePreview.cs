using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessagePreview : MonoBehaviour
{
    private Order _order;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text previewText;
    [SerializeField] private Button button;
    private Message message;

    private void Awake()
    {
        button.onClick.AddListener(OnButtonClick);
        message = FindAnyObjectByType<Message>();
    }

    public void SetOrder(Order order)
    {
        _order = order;
        nameText.text = order.MessageData.preview;
        nameText.text = $"{_order.ClientName} {order.ClientSurname}";
        previewText.text = order.MessageData.preview;

    }

    private void OnButtonClick()
    {
        GameObject.Find("Messages").SetActive(false);
        
        message.OpenMessage(_order);
        
    }
}