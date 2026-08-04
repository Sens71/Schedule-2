using System;
using TMPro;
using UnityEngine;
using UnityEditor;

public class TimeManager : MonoBehaviour
{
    public TMP_Text timeText;
    public float secondsPerHour;
    public Light directionLight;
    public TimePeriod[] timePeriods;
    private float time;
    private int hours;
    private int minutes;

    public int days;
    


    private void Awake()
    {
        time = 7 * secondsPerHour;
        Light directionlLight = GameObject.Find("Directional Light").GetComponent<Light>();
        AudioSource audio = GetComponent<AudioSource>();
        foreach (TimePeriod timePeriod in timePeriods)
        {
            timePeriod.InitSettings(secondsPerHour, audio, directionlLight);
        }
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= secondsPerHour * 24)
        {
            time = 0;
            days++;
        }
        hours = (int)(time / secondsPerHour);
        minutes = (int)((time % secondsPerHour) / (secondsPerHour / 60));
        timeText.text = $"{days}:{hours}:{minutes}";
        foreach (TimePeriod timePeriod in timePeriods)
        {
            timePeriod.ProgressTime(time);
        }
    }

    public ClockTime GetCurrentTime()
    {
        ClockTime clockTime = new ClockTime();
        clockTime.days = days;
        clockTime.hours = hours;
        clockTime.minutes = minutes;
        timeText.text = $"{days}:{hours}:{minutes}";
        return clockTime;
    }
    public bool WithinPeriod(ClockTime start, ClockTime end)
    {
        float startTime = start.hours * secondsPerHour + start.minutes * secondsPerHour / 60;
        float endTime = end.hours * secondsPerHour + end.minutes * secondsPerHour / 60;

        bool currentlyInPeriod;

        if (startTime < endTime)
        {
            currentlyInPeriod = time >= startTime && time < endTime;
        }
        else
        {
            float adjustedTime = time < startTime ? time + 24 * secondsPerHour : time;
            float adjustedEnd = endTime + 24 * secondsPerHour;
            currentlyInPeriod = adjustedTime >= startTime && adjustedTime < adjustedEnd;
        }
        return currentlyInPeriod;
    }
    public float GetRealTime(ClockTime time)
    {
        float realTime = time.hours * secondsPerHour + time.minutes * secondsPerHour / 60;
        return realTime;
    }
}

[Serializable]
public struct ClockTime
{
    public int days;
    public int hours;
    public int minutes;

    public static ClockTime operator +(ClockTime a, ClockTime b)
    {
        ClockTime clockTime = new ClockTime();
        int x = a.minutes + b.minutes;
        clockTime.minutes = x % 60;
        int y = a.hours + b.hours + (int)x/60;
        clockTime.hours = y % 24;
        int z = a.days + b.days + (int)y/24;
        clockTime.days = z;
        return clockTime;
        
    }
    public static ClockTime operator -(ClockTime a, ClockTime b)
    {
        ClockTime clockTime = new ClockTime();
        int totalMinutesA = a.days * 24 * 60 + a.hours * 60 + a.minutes;
        int totalMinutesB = b.days * 24 * 60 + b.hours * 60 + b.minutes;
        int diff = totalMinutesA - totalMinutesB;
        clockTime.days = diff / (24 * 60);
        clockTime.hours = (diff / 60) % 24;
        clockTime.minutes = diff % 60;
        return clockTime;
    }
    public static bool operator >(ClockTime a, ClockTime b)
    {
        if (a.days != b.days)
            return a.days > b.days;
        if (a.hours != b.hours)
            return a.hours > b.hours;
        return a.minutes > b.minutes;
         
        
    }
    public static bool operator <(ClockTime a, ClockTime b)
    {
        if (a.days != b.days)
            return a.days < b.days;
        if (a.hours != b.hours)
            return a.hours < b.hours;
        return a.minutes < b.minutes;
         
        
    }
    public static bool operator ==(ClockTime a, ClockTime b)
    {
        if (a.days != b.days|| a.hours != b.hours || a.minutes != b.minutes)
            return false;
        return true;
         
        
    }
    public static bool operator !=(ClockTime a, ClockTime b)
    {
        if (a.days != b.days|| a.hours != b.hours || a.minutes != b.minutes)
            return true;
        return false;
         
        
    }
    
    

    public override string ToString()
    {
        return $"{days}:{hours}:{minutes}";
    }
}
[CustomPropertyDrawer(typeof(ClockTime))]
public class ClockTimeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var hoursProp = property.FindPropertyRelative("hours");
        var minutesProp = property.FindPropertyRelative("minutes");
        var daysProp =  property.FindPropertyRelative("days");

        EditorGUI.LabelField(position, GUIContent.none);

        float labelWidth = 40f;
        float fieldWidth = (position.width - labelWidth * 3) / 3;
        
        Rect daysLabelRect    = new Rect(position.x,                    position.y, labelWidth, position.height);
        Rect daysFieldRect    = new Rect(daysLabelRect.xMax,            position.y, fieldWidth, position.height);

        Rect hoursLabelRect   = new Rect(daysFieldRect.xMax,            position.y, labelWidth, position.height);
        Rect hoursFieldRect   = new Rect(hoursLabelRect.xMax,           position.y, fieldWidth, position.height);

        Rect minutesLabelRect = new Rect(hoursFieldRect.xMax,           position.y, labelWidth, position.height);
        Rect minutesFieldRect = new Rect(minutesLabelRect.xMax,         position.y, fieldWidth, position.height);

        EditorGUI.LabelField(daysLabelRect,    "Days");
        daysProp.intValue    = EditorGUI.IntField(daysFieldRect,    GUIContent.none, daysProp.intValue);

        EditorGUI.LabelField(hoursLabelRect,   "Hours");
        hoursProp.intValue   = EditorGUI.IntField(hoursFieldRect,   GUIContent.none, hoursProp.intValue);
        
        EditorGUI.LabelField(minutesLabelRect, "Minutes");
        minutesProp.intValue = EditorGUI.IntField(minutesFieldRect, GUIContent.none, minutesProp.intValue);
    }
}
