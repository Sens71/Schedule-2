using UnityEngine;

[CreateAssetMenu(fileName = "Storage", menuName = "MyData/Storage")]
public class Storage : ScriptableObject
{
    public ItemData[] items;
}
