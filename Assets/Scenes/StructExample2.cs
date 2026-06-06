using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class StructExample2: MonoBehaviour
{
    public void DoStuff(Func<bool> action)
    { 
            if (action())
            {
                print("Time is odd");
            }
            else
            {
                print("Time is even");
            }


    }
    
}