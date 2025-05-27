using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float secondsPerHour = 30;
    public float time;
    public TMP_Text timeText;
    private int hours;
    private int minutes;
    void Start()
    {
        
    }

    
    void Update()
    {
        time += Time.deltaTime;
        if(time > secondsPerHour * 24)
        {
             time = 0;
        }
        hours = (int)(time / secondsPerHour);
        minutes = (int)(time % secondsPerHour);
        timeText.text = $"{hours} : {minutes}";
    }
}
