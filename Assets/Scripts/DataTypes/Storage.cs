using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Storage", menuName = "MyData/Storage")]
public class Storage : ScriptableObject
{
    public ItemData[] items;
    [SerializeField]private List<CanDrug> canDrugs = new();

    public void AddCanDrug(CanDrug drug)
    {
        canDrugs.Add(drug);
    }

    public void RemoveCanDrug(CanDrug drug)
    {
        
    }
}
