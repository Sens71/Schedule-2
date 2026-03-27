using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderFactory : MonoBehaviour
{
   [SerializeField] private OrderDataSamples data;
   private TimeManager timeManager; 
   public List<Order> orders = new();
   [SerializeField] private Vector2 randomItemAmount;
   [SerializeField] private Vector2 priceRange;
   [SerializeField] private Vector2 bargainRange;
   [SerializeField] private ClockTime minTime;
   [SerializeField] private ClockTime maxTime;
   [SerializeField] private PlayerMessageData[] playerMessageData;
   private ClockTime randomTime;
   public event Action<Order> OnOrderCreated;
   public event Action<Order> OnOrderExpired;

   private void Awake()
   {
       timeManager = FindAnyObjectByType<TimeManager>();
   }

   private Order GenerateOrder()
   {
      var order = new Order();
      int random = Random.Range(0, data.MessageData.Length);
      var messageData = Instantiate(data.MessageData[random]);
      order.MessageData = messageData;
      random = Random.Range(0, playerMessageData.Length);
      order.PlayerMessageData = playerMessageData[random];
      random = Random.Range(0, data.Items.Length);
      order.ItemData = data.Items[random];
      random = (int)Random.Range(randomItemAmount.x,randomItemAmount.y);
      order.Amount = random;
      float priceRandom = Random.Range(priceRange.x, priceRange.y);
      order.Price = (int)(priceRandom * order.ItemData.price * order.Amount);
      float x = Random.Range(bargainRange.x, bargainRange.y);
      float barRate = (priceRange.y - priceRandom)* x;
      order.MaxBargain = (int)(order.Price * (1 + barRate));
      random = Random.Range(0, data.ClientNames.Length);
      order.ClientName = data.ClientNames[random];
      random = Random.Range(0, data.ClientSurnames.Length);
      order.ClientSurname = data.ClientSurnames[random];
      random = Random.Range(0, data.ClientNumbers.Length);
      order.ClientNumber = data.ClientNumbers[random];
      order.dateTaken = timeManager.GetCurrentTime();
      random = Random.Range(3,5);
      order.dateExpires = new ClockTime()
      {
          hours = timeManager.GetCurrentTime().hours, minutes = timeManager.GetCurrentTime().minutes,
          days = timeManager.GetCurrentTime().days + random
      };
      InsertData(order);
      orders.Add(order);
      OnOrderCreated?.Invoke(order);
      
      return order;
   }

   private void InsertData(Order order)
   {
       order.MessageData.preview = order.MessageData.preview.Replace("{item}", order.ItemData.name);
       order.MessageData.introduction = order.MessageData.introduction.Replace("{amount}", order.Amount.ToString());
       order.MessageData.introduction = order.MessageData.introduction.Replace("{price}", order.Price.ToString());
       order.MessageData.introduction = order.MessageData.introduction.Replace("{item}", order.ItemData.name);
       
       
   }
   private void GenerateRandomTime()
   {
       var min = minTime.days * 24 * 60 +  minTime.hours * 60 + minTime.minutes;
       var max = maxTime.days * 24 * 60 +  maxTime.hours * 60 + maxTime.minutes;
       var random =  Random.Range(min, max);

       ClockTime newTime = new ClockTime();
       newTime.days = (int)random/ 1440;
       newTime.hours = (int)(random % 1440)/60;
       newTime.minutes = random % 60;

       newTime = newTime + timeManager.GetCurrentTime();
       randomTime = newTime;
   }
   private void Update()
   {
       if (timeManager.GetCurrentTime() > randomTime)
       {
           GenerateRandomTime();
           GenerateOrder();
       }
       
       for (int i = 0; i < orders.Count; i++)
       {
           if (orders[i].dateExpires < timeManager.GetCurrentTime())
           {
               OnOrderExpired?.Invoke(orders[i]);
               orders.Remove(orders[i]);
           }
       }
   }
}