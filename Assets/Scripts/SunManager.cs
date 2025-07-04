using UnityEngine;

public class SunManager : MonoBehaviour
{
    public TimeManager time;
    private float rotationPerSecond;
    void Start()
    {
        rotationPerSecond = 360 / time.secondsPerHour * 24;
    }

    
    void Update()
    {
        transform.Rotate(rotationPerSecond * Time.deltaTime,0,0);
    }
}
