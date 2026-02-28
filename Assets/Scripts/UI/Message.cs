using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Message : MonoBehaviour
{
   [SerializeField] private GameObject messageSent;
   [SerializeField] private GameObject messageReceived;
   [SerializeField] private TMP_Text phoneNumber;
   [SerializeField] private GameObject messagePanel;
   [SerializeField] private Transform content;
   [SerializeField] private GameObject messagePreviewPanel;
   private Order _order;

   public void OpenMessage(Order order)
   {
      _order = order;
      messagePanel.SetActive(true);

      switch (_order.State)
      {
         case  MessageState.Intruduction:
            GenerateMessage(order.MessageData.introduction,true);
            break;
         case  MessageState.AcceptTask:
            GenerateMessage(order.MessageData.introduction,true);
            GenerateMessage(order.MessageData.acceptTask,true);
            break;
         case  MessageState.RejectTask:
            GenerateMessage(order.MessageData.introduction,true);
            GenerateMessage(order.MessageData.rejectTask,true);
            break;
         case  MessageState.BargainPositive:
            GenerateMessage(order.MessageData.introduction,true);
            GenerateMessage(order.MessageData.bargainPositive,true);
            break;
         case  MessageState.BargainNegative:
            GenerateMessage(order.MessageData.introduction,true);
            GenerateMessage(order.MessageData.bargainNegative,true);
            break;
         case MessageState.DealOverPositiveBargain:
            GenerateMessage(order.MessageData.introduction,true);
            GenerateMessage(order.MessageData.bargainPositive,true);
            GenerateMessage(order.MessageData.dealOverPositive,true);
            break;
         case MessageState.DealOverNegativeBaragain:
            GenerateMessage(order.MessageData.introduction,true);
            GenerateMessage(order.MessageData.bargainPositive,true);
            GenerateMessage(order.MessageData.dealOverNegative,true);
            break;
         case MessageState.DealOverPositiveNoBargain:
            GenerateMessage(order.MessageData.introduction,true);
            GenerateMessage(order.MessageData.acceptTask,true);
            GenerateMessage(order.MessageData.dealOverPositive,true);
            break;
         case MessageState.DealOverNegativeNoBargain:
            GenerateMessage(order.MessageData.introduction,true);
            GenerateMessage(order.MessageData.acceptTask,true);
            GenerateMessage(order.MessageData.dealOverNegative,true);
            break;
      }
   }
   private void GenerateMessage(string message, bool reciever)
   {
      GameObject instance;
      if (reciever)
      {
         instance = Instantiate(messageReceived, content);
      }
      else
      {
         instance = Instantiate(messageSent, content);
      }
      var text = instance.GetComponentInChildren<TMP_Text>();
      text.text = message;
   }

   public void CloseMessage()
   {
      for (int i = content.childCount - 1; i >= 0; i--)
      {
         var child = content.GetChild(i).gameObject;
         Destroy(child);
      }
      messagePanel.SetActive(false);
      messagePreviewPanel.SetActive(true);
   }
}

public enum MessageState
{
   Intruduction,
   AcceptTask,
   RejectTask,
   DealOverPositiveNoBargain,
   DealOverNegativeNoBargain,
   DealOverPositiveBargain,
   DealOverNegativeBaragain,
   BargainPositive,
   BargainNegative
   
   
   
   
}
