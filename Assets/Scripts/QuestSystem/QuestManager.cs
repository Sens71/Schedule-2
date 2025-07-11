using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests = new();
    public List<GameObject> questPanels = new();

    private void Awake()
    {
        foreach (var quest in quests)
        {
            quest.OnDataUpdate += UpdateUI;
        }

    }
    private void UpdateUI()
    {
        questPanels.ForEach(obj => obj.SetActive(false));

        int panelIndex = 0;
        foreach (var quest in quests)
        {
            if (quest.QuestData.state.Value == QuestState.Ready)
            {
                if (panelIndex >= questPanels.Count)
                    break;

                var panel = questPanels[panelIndex];
                panel.SetActive(true);
                panel.GetComponentInChildren<TMP_Text>().text = quest.QuestData.name.Value;

                panelIndex++;
            }
        }
    }
}
