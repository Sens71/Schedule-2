using UnityEngine;

public class RotationBlocker : MonoBehaviour
{

    void Update()
    {
       transform.rotation = Quaternion.Euler(90f, 0f, 0f); 
    }
}
