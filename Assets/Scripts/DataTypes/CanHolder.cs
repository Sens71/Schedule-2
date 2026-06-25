using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CanHolder", menuName = "MyData/CanHolder")]
public class CanHolder : ScriptableObject
{
    public List<CanDrug> canDrugs = new List<CanDrug>();
}
