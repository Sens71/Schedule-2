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
    public List<ReagentData> reagents;
    public Storage storage;
    public void Evaluate()
    {
        var drug = new Drug(reagents.ToArray());
        finalColor = drug.iconColor;
        storage.AddDrug(drug);
    }
    private void Mixreagents(params ReagentData[] reagent)
    {
            
    }

}