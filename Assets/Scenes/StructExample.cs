using System;
using UnityEngine;

public class StructExample : MonoBehaviour
{
    private float timer;
    public StructExample2 actionTest;
    

    private void Start()
    {
        

    }

    private bool TimeIsOdd()
    {
        int time = (int)Time.time;
        return time % 2 == 0;
    }


    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < 1)
            return;
        Func<bool> timeOdd = TimeIsOdd;
        actionTest.DoStuff(timeOdd);
        timer = 0;
    }
    private void GoodBye(string message, int number)
    {
        
    }
}