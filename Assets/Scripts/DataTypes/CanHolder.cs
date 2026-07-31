using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CanHolder", menuName = "MyData/CanHolder")]
public class CanHolder : ScriptableObject
{
    public List<Drug> drugs = new List<Drug>();
}
