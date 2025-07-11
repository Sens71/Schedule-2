using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;

public class QuestActivator : MonoBehaviour
{
    [SerializeReference] [SerializeField]
    private List<ConditionBase> _triggerConditions = new();
    [SerializeReference][SerializeField]
    private List<ConditionBase> _activateConditions = new();
    [SerializeField] private QuestData _questData;
    private QuestProgressor _questProgressor;
    private bool _isActive;
    private Quest _quest;
    private Collider _collider;
    private MeshRenderer _meshRenderer;
    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _questProgressor = GetComponent<QuestProgressor>();
        _quest = _questProgressor.quest;
    }
    private void Update()
    {
        if (CheckConditionsList(_activateConditions) && _quest.CompareData(_questData))
        {
            if (_isActive == false)
                SetObjectActive(true);
            
            if (CheckConditionsList(_triggerConditions))
            {
                _questProgressor.ProgressQuest();
            }
        }
        else
        {
            SetObjectActive(false);
        }
    }
    private bool CheckConditionsList(List<ConditionBase> conditions)
    {
        foreach (var condition in conditions)
        {
            if (!condition.CheckCondition())
                return false;
        }
        return true; 
    }

    private void SetObjectActive(bool active)
    {
        if(_collider != null) _collider.enabled = active;
        if(_meshRenderer != null) _meshRenderer.enabled = active;
        _isActive = active;
    }
}
[CustomEditor(typeof(QuestActivator))]
public class QuestActivatorEditor : Editor
{
    private ReorderableList _triggerConditionList;
    private SerializedProperty _triggerConditionsProp;

    private ReorderableList _activateConditionList; 
    private SerializedProperty _activateConditionsProp; 

    private List<Type> _conditionTypes;
    private string[] _conditionTypeNames;

    private void OnEnable()
    {
        _triggerConditionsProp = serializedObject.FindProperty("_triggerConditions");
        _activateConditionsProp = serializedObject.FindProperty("_activateConditions");

        _conditionTypes = TypeCache.GetTypesDerivedFrom<ConditionBase>()
            .Where(t => !t.IsAbstract && !t.IsSubclassOf(typeof(UnityEngine.Object)))
            .ToList();
        _conditionTypeNames = _conditionTypes.Select(t => t.Name).ToArray();

        _triggerConditionList = new ReorderableList(serializedObject, _triggerConditionsProp, true, true, true, true) 
        {
            drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Trigger Conditions"),
            drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var element = _triggerConditionsProp.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(rect, element, GUIContent.none, true);
            },
            elementHeightCallback = index =>
            {
                var element = _triggerConditionsProp.GetArrayElementAtIndex(index);
                return EditorGUI.GetPropertyHeight(element, true) + 4;
            },
            onAddCallback = list => ShowConditionTypeMenu(list.serializedProperty),
            onRemoveCallback = list =>{
                var element = _triggerConditionsProp.GetArrayElementAtIndex(list.index);
                var condition = element.managedReferenceValue as ConditionBase;
                condition?.OnRemove();

                _triggerConditionsProp.DeleteArrayElementAtIndex(list.index);
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(target);
            }
        };

        _activateConditionList = new ReorderableList(serializedObject, _activateConditionsProp, true, true, true, true) 
        {
            drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Activate Conditions"),
            drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var element = _activateConditionsProp.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(rect, element, GUIContent.none, true);
            },
            elementHeightCallback = index =>
            {
                var element = _activateConditionsProp.GetArrayElementAtIndex(index);
                return EditorGUI.GetPropertyHeight(element, true) + 4;
            },
            onAddCallback = list => ShowConditionTypeMenu(list.serializedProperty),
            onRemoveCallback = list =>{
                var element = _activateConditionsProp.GetArrayElementAtIndex(list.index);
                var condition = element.managedReferenceValue as ConditionBase;
                condition?.OnRemove();

                _activateConditionsProp.DeleteArrayElementAtIndex(list.index);
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(target);
            }
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(serializedObject, "_triggerConditions", "_activateConditions", "m_Script");

        GUILayout.Space(10);
        _triggerConditionList.DoLayoutList(); 

        GUILayout.Space(10);
        _activateConditionList.DoLayoutList(); 

        serializedObject.ApplyModifiedProperties();
    }

    private void ShowConditionTypeMenu(SerializedProperty targetProperty)
    {
        serializedObject.Update(); 
        var menu = new GenericMenu();

        for (int i = 0; i < _conditionTypes.Count; i++)
        {
            int localIndex = i;
            string typeName = _conditionTypeNames[i];

            menu.AddItem(new GUIContent(typeName), false, () =>
            {
                var instance = Activator.CreateInstance(_conditionTypes[localIndex]);

                Undo.RecordObject(target, "Add Condition");

                targetProperty.arraySize++;
                serializedObject.ApplyModifiedProperties();

                serializedObject.Update();
                var newElement = targetProperty.GetArrayElementAtIndex(targetProperty.arraySize - 1);
                newElement.managedReferenceValue = instance;

                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(target);
            });
        }

        menu.ShowAsContext();
    }

    private void DrawPropertiesExcluding(SerializedObject obj, params string[] excludedProperties)
    {
        var iterator = obj.GetIterator();
        bool enterChildren = true;
        while (iterator.NextVisible(enterChildren))
        {
            enterChildren = false;
            if (!excludedProperties.Contains(iterator.name))
            {
                EditorGUILayout.PropertyField(iterator, true);
            }
        }
    }
}



