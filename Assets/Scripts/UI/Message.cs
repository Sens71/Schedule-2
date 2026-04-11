using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
   [SerializeField] private MessageComponents messageSent;
   [SerializeField] private MessageComponents messageReceived;
   [SerializeField] private TMP_Text phoneNumber;
   [SerializeField] private GameObject messagePanel;
   [SerializeField] private Transform content;
   [SerializeField] private GameObject messagePreviewPanel;
   [SerializeField] private MessageOption optionPrefab;
   [SerializeField] private BargainMenu bargainMenuPrefab;
   private MessageOption option;
   private Order _order;
   private BargainMenu _bargainMenu;
   private TimeManager _timeManager;

   private void Awake()
   {
      _timeManager = FindAnyObjectByType<TimeManager>();
   }

   public void OpenMessage(Order order)
   {
      _order = order;
      messagePanel.SetActive(true);

      switch (_order.State)
      {
         case  MessageState.Intruduction:
            GenerateMessage(order.MessageData.introduction,true, order.dateTaken);
            option = Instantiate(optionPrefab, content);
            LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());
            Canvas.ForceUpdateCanvases();
            option.accept.onClick.AddListener(AcceptTask);
            option.reject.onClick.AddListener(RejectTask);
            option.bargain.onClick.AddListener(TryBargain);
            break;
         case  MessageState.AcceptTask:
            GenerateMessage(order.MessageData.introduction,true, order.dateTaken);
            GenerateMessage(order.PlayerMessageData.acceptTask,false,order.dateResponded);
            GenerateMessage(order.MessageData.acceptTask,true, order.dateResponded);
            break;
         case  MessageState.RejectTask:
            GenerateMessage(order.MessageData.introduction,true, order.dateTaken);
            GenerateMessage(order.PlayerMessageData.rejectTask,false, order.dateResponded);
            GenerateMessage(order.MessageData.rejectTask,true, order.dateResponded);
            break;
         case  MessageState.BargainPositive:
            GenerateMessage(order.MessageData.introduction,true, order.dateTaken);
            GenerateMessage(order.MessageData.bargainPositive,true, order.dateResponded);
            break;
         case  MessageState.BargainNegative:
            GenerateMessage(order.MessageData.introduction,true, order.dateTaken);
            GenerateMessage(order.PlayerMessageData.rejectBargain,false, order.dateResponded);
            GenerateMessage(order.MessageData.bargainNegative,true, order.dateResponded);
            break;
         case MessageState.DealOverPositiveBargain:
            GenerateMessage(order.MessageData.introduction,true,order.dateTaken);
            GenerateMessage(order.MessageData.bargainPositive,true, order.dateResponded);
            GenerateMessage(order.MessageData.dealOverPositive,true, order.dateResponded);
            break;
         case MessageState.DealOverNegativeBaragain:
            GenerateMessage(order.MessageData.introduction,true, order.dateTaken);
            GenerateMessage(order.MessageData.bargainPositive,true, order.dateResponded);
            GenerateMessage(order.MessageData.dealOverNegative,true, order.dateFinished);
            break;
         case MessageState.DealOverPositiveNoBargain:
            GenerateMessage(order.MessageData.introduction,true,order.dateTaken);
            GenerateMessage(order.MessageData.acceptTask,true,order.dateResponded);
            GenerateMessage(order.MessageData.dealOverPositive,true, order.dateFinished);
            break;
         case MessageState.DealOverNegativeNoBargain:
            GenerateMessage(order.MessageData.introduction,true, order.dateTaken);
            GenerateMessage(order.MessageData.acceptTask,true, order.dateResponded);
            GenerateMessage(order.MessageData.dealOverNegative,true,order.dateFinished);
            break;
      }

      if (option != null)
      {
         EditorUtility.SetDirty(option);
      }
      
   }
   private void GenerateMessage(string message, bool reciever, ClockTime time)
   {
      MessageComponents instance;
      if (reciever)
      {
         instance = Instantiate(messageReceived, content);
      }
      else
      {
         instance = Instantiate(messageSent, content);
      }
      instance.MessageText.text = message;
      instance.DateText.text = time.ToString();
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

   public void AcceptTask()
   {
      _order.dateResponded = _timeManager.GetCurrentTime();
      Destroy(option.gameObject);
      _order.State = MessageState.AcceptTask;
      GenerateMessage(_order.PlayerMessageData.acceptTask,false, _order.dateResponded);
      GenerateMessage(_order.MessageData.acceptTask,true, _order.dateResponded);
   }

   public void RejectTask()
   {
      _order.dateResponded = _timeManager.GetCurrentTime();
      Destroy(option.gameObject);
      _order.State = MessageState.RejectTask;
      GenerateMessage(_order.PlayerMessageData.rejectTask,false, _order.dateResponded);
      GenerateMessage(_order.MessageData.rejectTask,true,_order.dateResponded);
   }
   public void TryBargain()
   {
      Destroy(option.gameObject);
      _bargainMenu = Instantiate(bargainMenuPrefab, content);
      _bargainMenu.SetData(_order,MakeDealCallback,CancelBargainCallback);
   }

   private void MakeDealCallback()
   {
      Destroy(_bargainMenu.gameObject);
      if (_order.OfferedPrice <= _order.MaxBargain)
      {
         _order.State = MessageState.BargainPositive;
         _order.MessageData.bargainPositive = _order.MessageData.bargainPositive.Replace("{price}", _order.OfferedPrice.ToString());
         GenerateMessage(_order.MessageData.bargainPositive,true, _order.dateResponded);
      }
      else
      {
         _order.State = MessageState.BargainNegative;
         _order.MessageData.bargainNegative = _order.MessageData.bargainNegative.Replace("{price}", _order.OfferedPrice.ToString());         
         GenerateMessage(_order.PlayerMessageData.rejectBargain,false, _order.dateResponded);
         GenerateMessage(_order.MessageData.bargainNegative,true, _order.dateResponded);
      }
   }

   private void CancelBargainCallback()
   {
      Destroy(_bargainMenu.gameObject);
      option = Instantiate(optionPrefab, content);
      option.accept.onClick.AddListener(AcceptTask);
      option.reject.onClick.AddListener(RejectTask);
      option.bargain.onClick.AddListener(TryBargain);
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
