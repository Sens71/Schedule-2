using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EvaluatorData))]
public class EvaluatorDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EvaluatorData data = (EvaluatorData)target;

        if (GUILayout.Button("Evaluate", GUILayout.Height(30)))
        {
            data.Evaluate();
            EditorUtility.SetDirty(data);
        }



        EditorGUILayout.Space();
        DrawDefaultInspector();
    }
}