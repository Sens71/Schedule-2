using UnityEngine;

[CreateAssetMenu(fileName = "Order Data Samples", menuName = "MyData/Order Data Samples")]
public class OrderDataSamples : ScriptableObject
{
    public MessageData[] MessageData;
    public ItemData[] Items;
    public string[] ClientNames;
    public string[] ClientSurnames;
    public string[] ClientNumbers;
}
