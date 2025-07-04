using System;
using UnityEngine;

public class QuestActivator : MonoBehaviour
{
    [SerializeField] private QuestData _questData;
    [SerializeField] private ConditionBase[] _conditions; 
    private QuestProgressor _questProgressor;
    private bool _isActive;
    private Quest _quest;
    private Collider _collider;
    private MeshRenderer _meshRenderer;
    private void Start()
    {
        _collider = GetComponent<Collider>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _quest = _questProgressor.quest;
    }
    private void Update()
    {
        if (CheckOptionalConditions() && _quest.CompareData(_questData))
        {
            if (_isActive == false)
                SetObjectActive(true);
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                _questProgressor.ProgressQuest();
            }
        }
        else
        {
            SetObjectActive(false);
        }
    }
    private bool CheckOptionalConditions()
    {
        foreach (var condition in _conditions)
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
