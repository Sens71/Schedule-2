using UnityEngine;

public class StructExample : MonoBehaviour
{
    private Vector3 startPos;
    private MyStruct myStruct;
    private Collider myCollider;
    private Rigidbody myRigidbody;
    private string myName;
    private int[] intArray = new int[3];

    void Start()
    {
        
        intArray[0] = 0;
        intArray[1] = 1;
        intArray[2] = 2;
        ChangeArray(intArray);
        foreach (int i in intArray)
        {
            print(i);
        }
    }


    void Update()
    {

    }

    private void ChangeArray(int[] array)
    {
        array[0] = 1;
        foreach (int i in array)
        {
            print(i);
        }
    }
}

public struct MyStruct
{
    
}
