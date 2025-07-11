using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public struct Param<T>
{
    public bool Use;
    public T Value;
    public QuestParamCompareSign CompareSign;
    public QuestParamChangeSign ChangeSign;
    public Type Type => typeof(T);
}
public enum QuestParamCompareSign
{
    None,
    GreaterThan,
    LessThan,
    Equal,
    NotEqual,
    GreaterOrEqual,
    LessOrEqual,
}
public enum QuestParamChangeSign
{
    None,
    Add,
    Subtract,
    Set
}

[CustomPropertyDrawer(typeof(Param<>))]
public class OptionalDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty useProp = property.FindPropertyRelative("Use");  
        SerializedProperty valueProp = property.FindPropertyRelative("Value");

        float fullWidth = position.width;
        float labelWidth = EditorGUIUtility.labelWidth;
        float toggleWidth = 18f;
        float spacing = 4f;
        float fieldWidth = fullWidth - labelWidth - toggleWidth - spacing;

        var labelRect = new Rect(position.x, position.y, labelWidth, position.height);
        var toggleRect = new Rect(labelRect.xMax, position.y, toggleWidth, position.height);
        var valueRect = new Rect(toggleRect.xMax + spacing, position.y, fieldWidth, position.height);

        EditorGUI.LabelField(labelRect, label);
        useProp.boolValue = EditorGUI.Toggle(toggleRect, useProp.boolValue);

        GUI.enabled = useProp.boolValue;
        EditorGUI.PropertyField(valueRect, valueProp, GUIContent.none);
        GUI.enabled = true;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property.FindPropertyRelative("Value"), label, true);
    }
}