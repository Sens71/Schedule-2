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
        }
        hours = (int)(time / secondsPerHour);
        minutes = (int)((time % secondsPerHour) / (secondsPerHour / 60));
        timeText.text = $"{hours}:{minutes}";
        foreach (TimePeriod timePeriod in timePeriods)
        {
            timePeriod.ProgressTime(time);
        }
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
    public int hours;
    public int minutes;
}
[CustomPropertyDrawer(typeof(ClockTime))]
public class ClockTimeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var hoursProp = property.FindPropertyRelative("hours");
        var minutesProp = property.FindPropertyRelative("minutes");

        EditorGUI.LabelField(position, GUIContent.none);

        float labelWidth = 50f;
        float fieldWidth = (position.width - labelWidth * 2) / 2;

        Rect hoursLabelRect = new Rect(position.x, position.y, labelWidth, position.height);
        Rect hoursFieldRect = new Rect(hoursLabelRect.xMax, position.y, fieldWidth, position.height);
        Rect minutesLabelRect = new Rect(hoursFieldRect.xMax, position.y, labelWidth, position.height);
        Rect minutesFieldRect = new Rect(minutesLabelRect.xMax, position.y, fieldWidth, position.height);

        EditorGUI.LabelField(hoursLabelRect, "Hours");
        hoursProp.intValue = EditorGUI.IntField(hoursFieldRect, GUIContent.none, hoursProp.intValue);

        EditorGUI.LabelField(minutesLabelRect, "Minutes");
        minutesProp.intValue = EditorGUI.IntField(minutesFieldRect, GUIContent.none, minutesProp.intValue);
    }
}
