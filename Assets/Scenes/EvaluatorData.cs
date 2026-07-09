using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[CreateAssetMenu(fileName = "EvaluatorData", menuName = "Data/EvaluatorData")]
public class EvaluatorData : ScriptableObject
{
    public List<CanDrug> drugs;
    public List<ReagentData> reagents;
    public void Evaluate()
    {
        var drug = new CanDrug(reagents.ToArray());
        drugs.Add(drug);
    }
    private void Mixreagents(params ReagentData[] reagent)
    {
            
    }

}