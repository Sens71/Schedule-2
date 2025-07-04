using System;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(QuestActions))]
[RequireComponent(typeof(QuestActivator))]
public class QuestProgressor : MonoBehaviour
{
    public Quest quest;
    [SerializeField] private QuestData _questData;

    public void ProgressQuest()
    {
        quest.ChangeData(_questData);
    }
}