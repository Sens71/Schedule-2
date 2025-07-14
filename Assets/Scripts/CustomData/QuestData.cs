using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;

[Serializable]
public class QuestData
{
    public QuestDataDrawMode EditorDrawMode;
    public Param<int> id;
    public Param<int> stage;
    public Param<string> name;
    public Param<Transform> position;
    public Param<QuestState> state;
    public Param<string> description;

    public void ApplyChangesFrom(QuestData other)
    {
        if (other.id.Use)
        {
            switch (other.id.ChangeSign)
            {
                case QuestParamChangeSign.Add:
                    id.Value += other.id.Value;
                    break;
                case QuestParamChangeSign.Subtract:
                    id.Value -= other.id.Value;
                    break;
                case QuestParamChangeSign.Set:
                    id.Value = other.id.Value;
                    break;
            }
        }
        if (other.stage.Use)
        {
            switch (other.stage.ChangeSign)
            {
                case QuestParamChangeSign.Add:
                    stage.Value += other.stage.Value;
                    break;
                case QuestParamChangeSign.Subtract:
                    stage.Value -= other.stage.Value;
                    break;
                case QuestParamChangeSign.Set:
                    stage.Value = other.stage.Value;
                    break;
            }
        }
        if (other.name.Use)
        {
            switch (other.name.ChangeSign)
            {
                case QuestParamChangeSign.Add:
                    name.Value += other.name.Value;
                    break;
                case QuestParamChangeSign.Set:
                    name.Value = other.name.Value;
                    break;
            }
        }
        if (other.position.Use)
        {
            switch (other.position.ChangeSign)
            {
                case QuestParamChangeSign.Set:
                    position.Value = other.position.Value;
                    break;
            }
        }
        if (other.state.Use)
        {
            switch (other.state.ChangeSign)
            {
                case QuestParamChangeSign.Set:
                    state.Value = other.state.Value;
                    break;
            }
        }
        if (other.description.Use)
        {
            switch (other.description.ChangeSign)
            {
                case QuestParamChangeSign.Add:
                    description.Value += other.description.Value;
                    break;
                case QuestParamChangeSign.Set:
                    description.Value = other.description.Value;
                    break;
            }
        }
    }
    public bool CompareTo(QuestData other)
    {
        if (other.id.Use)
        {
            switch (other.id.CompareSign)
            {
                case QuestParamCompareSign.Equal:
                    if (id.Value != other.id.Value) return false;
                    break;
                case QuestParamCompareSign.NotEqual:
                    if (id.Value == other.id.Value) return false;
                    break;
                case QuestParamCompareSign.GreaterThan:
                    if (id.Value <= other.id.Value) return false;
                    break;
                case QuestParamCompareSign.LessThan:
                    if (id.Value >= other.id.Value) return false;
                    break;
                case QuestParamCompareSign.GreaterOrEqual:
                    if (id.Value < other.id.Value) return false;
                    break;
                case QuestParamCompareSign.LessOrEqual:
                    if (id.Value > other.id.Value) return false;
                    break;
            }
        }
        if (other.stage.Use)
        {
            switch (other.stage.CompareSign)
            {
                case QuestParamCompareSign.Equal:
                    if (stage.Value != other.stage.Value) return false;
                    break;
                case QuestParamCompareSign.NotEqual:
                    if (stage.Value == other.stage.Value) return false;
                    break;
                case QuestParamCompareSign.GreaterThan:
                    if (stage.Value <= other.stage.Value) return false;
                    break;
                case QuestParamCompareSign.LessThan:
                    if (stage.Value >= other.stage.Value) return false;
                    break;
                case QuestParamCompareSign.GreaterOrEqual:
                    if (stage.Value < other.stage.Value) return false;
                    break;
                case QuestParamCompareSign.LessOrEqual:
                    if (stage.Value > other.stage.Value) return false;
                    break;
            }
        }
        if (other.name.Use)
        {
            switch (other.name.CompareSign)
            {
                case QuestParamCompareSign.Equal:
                    if (name.Value != other.name.Value) return false;
                    break;
                case QuestParamCompareSign.NotEqual:
                    if (name.Value == other.name.Value) return false;
                    break;
            }
        }
        if (other.position.Use)
        {
            switch (other.position.CompareSign)
            {
                case QuestParamCompareSign.Equal:
                    if (position.Value != other.position.Value) return false;
                    break;
                case QuestParamCompareSign.NotEqual:
                    if (position.Value == other.position.Value) return false;
                    break;
            }
        }
        if (other.state.Use)
        {
            switch (other.state.CompareSign)
            {
                case QuestParamCompareSign.Equal:
                    if (state.Value != other.state.Value) return false;
                    break;
                case QuestParamCompareSign.NotEqual:
                    if (state.Value == other.state.Value) return false;
                    break;
            }
        }
        if (other.description.Use)
        {
            switch (other.description.CompareSign)
            {
                case QuestParamCompareSign.Equal:
                    if (description.Value != other.description.Value) return false;
                    break;
                case QuestParamCompareSign.NotEqual:
                    if (description.Value == other.description.Value) return false;
                    break;
            }
        }

        return true;
    }
    public override string ToString()
    {
        return
            $"[id] Use: {id.Use}, Value: {id.Value}, Change: {id.ChangeSign}, Compare: {id.CompareSign}\n" +
            $"[name] Use: {name.Use}, Value: {name.Value}, Change: {name.ChangeSign}, Compare: {name.CompareSign}\n" +
            $"[position] Use: {position.Use}, Value: {position.Value}, Change: {position.ChangeSign}, Compare: {position.CompareSign}";
    }
}
[CustomPropertyDrawer(typeof(QuestData))]
public class QuestDataDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
{
    EditorGUI.BeginProperty(position, label, property);

    float y = position.y;
    float spacing = EditorGUIUtility.standardVerticalSpacing;

    // Отображение выпадающего списка DrawMode
    Rect popupRect = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
    SerializedProperty modeProp = property.FindPropertyRelative("EditorDrawMode");
    modeProp.enumValueIndex = (int)(QuestDataDrawMode)EditorGUI.EnumPopup(popupRect, "Draw Mode", (QuestDataDrawMode)modeProp.enumValueIndex);
    var drawMode = (QuestDataDrawMode)modeProp.enumValueIndex;
    y += EditorGUIUtility.singleLineHeight + spacing;

    var targetType = fieldInfo.FieldType;
    var fields = targetType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

    foreach (var field in fields)
    {
        if (!field.FieldType.IsGenericType || field.FieldType.GetGenericTypeDefinition() != typeof(Param<>))
            continue;

        var subProp = property.FindPropertyRelative(field.Name);
        if (subProp == null)
            continue;

        SerializedProperty use = subProp.FindPropertyRelative("Use");
        SerializedProperty value = subProp.FindPropertyRelative("Value");
        SerializedProperty compareSign = subProp.FindPropertyRelative("CompareSign");
        SerializedProperty changeSign = subProp.FindPropertyRelative("ChangeSign");

        float height = EditorGUIUtility.singleLineHeight;
        float fullHeight = height + spacing;

        Rect rowRect = new Rect(position.x, y, position.width, height);
        Rect labelRect = new Rect(rowRect.x, rowRect.y, 100, height);
        Rect toggleRect = new Rect(labelRect.xMax + 4, rowRect.y, 16, height);
        Rect extraRect = new Rect(toggleRect.xMax + 4, rowRect.y, 80, height);
        Rect valueRect = new Rect(extraRect.xMax + 4, rowRect.y, rowRect.width - (extraRect.xMax - rowRect.x) - 4, height);

        // Отрисовка названия параметра (всегда активно)
        EditorGUI.LabelField(labelRect, ObjectNames.NicifyVariableName(field.Name));

        // Отрисовка галочки Use
        EditorGUI.PropertyField(toggleRect, use, GUIContent.none);

        // Отключение полей, если Use = false
        EditorGUI.BeginDisabledGroup(!use.boolValue);

        if (drawMode == QuestDataDrawMode.Change)
            EditorGUI.PropertyField(extraRect, changeSign, GUIContent.none);
        else if (drawMode == QuestDataDrawMode.Compare)
            EditorGUI.PropertyField(extraRect, compareSign, GUIContent.none);

        EditorGUI.PropertyField(valueRect, value, GUIContent.none);

        EditorGUI.EndDisabledGroup();

        y += fullHeight;
    }

    EditorGUI.EndProperty();
}


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var targetType = fieldInfo.FieldType;
        var fields = targetType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        int count = fields.Count(f =>
            f.FieldType.IsGenericType && f.FieldType.GetGenericTypeDefinition() == typeof(Param<>));

        return (count + 1) * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
    }
}
public enum QuestDataDrawMode
{
    Default,
    Change,
    Compare
}

