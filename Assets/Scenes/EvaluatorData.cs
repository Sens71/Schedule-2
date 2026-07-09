using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[CreateAssetMenu(fileName = "EvaluatorData", menuName = "Data/EvaluatorData")]
public class EvaluatorData : ScriptableObject
{
    [SpritePreview(nameof(finalColor))]
    public Sprite icon;
    public Color finalColor;
    public List<CanDrug> drugs;
    public List<ReagentData> reagents;
    public void Evaluate()
    {
        var drug = new CanDrug(reagents.ToArray());
        drugs.Add(drug);
        finalColor = drug.iconColor;
    }
    private void Mixreagents(params ReagentData[] reagent)
    {
            
    }

}