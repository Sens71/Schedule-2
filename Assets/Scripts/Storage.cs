using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Storage", menuName = "MyData/Storage")]
public class Storage : ScriptableObject
{
    public ItemData money;
    public ItemData canSeed;
    public ItemData kSeed;
    public ItemData decSeed;
    public ItemData metSeed;
    public ItemData medPack;
}
[Serializable]
public class ItemData
{
    public string name;
    public Sprite icon;
    public int amount;
    public Color bgColor = Color.yellow;
}