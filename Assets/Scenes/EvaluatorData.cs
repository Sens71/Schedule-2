using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[CreateAssetMenu(fileName = "EvaluatorData", menuName = "Data/EvaluatorData")]
public class EvaluatorData : ScriptableObject
{

    public void Evaluate()
    {
        for (int i = 0; i < 10; i++)
        {
            if(i == 5)
                continue;
            Debug.Log(i);
        }
    }


}