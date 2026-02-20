using System;

[Serializable]
public class Order
{
   public MessageData MessageData;
   public ItemData ItemData;
   public string ClientName;
   public string ClientSurname;
   public string ClientNumber;
   public ClockTime dateTaken;
   public ClockTime dateExpires;
}