using UnityEngine;

public class StructExample : MonoBehaviour
{
    public ClockTime clockTimeA;
    public ClockTime clockTimeB;

    void Start()
    {
        print(clockTimeA==clockTimeB);
        print(clockTimeA>clockTimeB);
        print(clockTimeA<clockTimeB);
        print(clockTimeA!=clockTimeB);
        
    }


    void Update()
    {

    }

    
}


