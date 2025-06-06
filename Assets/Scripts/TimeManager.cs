using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float secondsPerHour = 30;
    public float secondsPerDay;
    public Quest quest;
    public float time;
    public TMP_Text timeText;
    public int hours;
    public int minutes;
    void Awake()
    {
        secondsPerDay = secondsPerHour * 24;
    }

    
    void Update()
    {

        time += Time.deltaTime;
        if(time > secondsPerHour * 24)
        {
             time = 0;
        }
        hours = (int)(time / secondsPerHour);
        quest.currentQuestProgress = hours;
        minutes = (int)(time % secondsPerHour) * (int)(60 / secondsPerHour);

        timeText.text = $"{hours} : {minutes}";
    }
}
