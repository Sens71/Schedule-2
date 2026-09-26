using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UIExample : MonoBehaviour
{
    private Button showButton;
    private Button hideButton;
    private VisualElement statsPanel;
    private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        statsPanel = root.Q<VisualElement>("Stats");
        showButton = root.Q<Button>("ShowStats");
        hideButton = root.Q<Button>("HideStats");
        
        showButton.clicked += ShowStats;
        hideButton.clicked += HideStats;
    }

    private void OnDestroy()
    {
        showButton.clicked -= ShowStats;
        hideButton.clicked -= HideStats;
    }

    private void ShowStats()
    {
        statsPanel.style.display = DisplayStyle.Flex;
    }

    private void HideStats()
    {
        statsPanel.style.display = DisplayStyle.None;
    }
}
