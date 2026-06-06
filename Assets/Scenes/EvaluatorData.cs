using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EvaluatorData", menuName = "Data/EvaluatorData")]
public class EvaluatorData : ScriptableObject
{
    

    public Func<int,int,string> text;
    public int c;
    public int b;
    public Func<int> testNumber;

    public void Evaluate()
    {
        testNumber = () => c + b;
        text = HelloWorld;
        string a = text?.Invoke(testNumber(),5);
        Debug.Log(a);
    }

    private string HelloWorld(int i, int g)
    {
        return "Hello World"  + i + g;
    }
}