using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "MyData/Item")]
public class ItemData: ScriptableObject
{
    public string name;
    public Sprite icon;
    public int amount;
    public Color bgColor = Color.yellow;

    public event Action OnChange;

    public void ChangeAmount(int amount)
    {
        this.amount += amount;
        OnChange?.Invoke();
    }
}