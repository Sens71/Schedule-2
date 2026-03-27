using System;

[Serializable]
public class Order
{
   public MessageData MessageData;
   public PlayerMessageData PlayerMessageData;
   public ItemData ItemData;
   public int Amount;
   public int Price;
   public int MaxBargain;
   public int OfferedPrice;
   public string ClientName;
   public string ClientSurname;
   public string ClientNumber;
   public ClockTime dateTaken;
   public ClockTime dateExpires;
   public MessageState State = MessageState.Intruduction;
}