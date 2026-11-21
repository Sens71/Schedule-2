using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ReagentReciever : MonoBehaviour
{
    private List<VisualElement> slots = new();
    private VisualElement doc;
    private VisualElement receiver;

    private void OnEnable()
    {
        doc = GetComponent<UIDocument>().rootVisualElement;
        receiver = doc.Q<VisualElement>("Receiver");
        slots.Clear();
        slots.Add(receiver.Q<VisualElement>("Slot1"));
        slots.Add(receiver.Q<VisualElement>("Slot2"));
        slots.Add(receiver.Q<VisualElement>("Slot3"));
        
        Drag.Released += OnReleased;
    }

    private void OnDisable()
    {
        Drag.Released -= OnReleased;
    }


    private void OnReleased(VisualElement source, Vector2 position)
    {
        VisualElement target = receiver.panel.Pick(position);
        print(target.name);
        if(!slots.Contains(target))
            return;
        
        if(source.userData is not ReagentData reagent)
            return;
        Show(target, reagent);
    }

    private void Show(VisualElement slot, ReagentData reagent)
    {
        VisualElement icon = slot.Q<VisualElement>("Icon");
        Label name = slot.Q<Label>("Name");

        icon.style.backgroundImage = new StyleBackground(reagent.icon);
        name.text = reagent.name;
    }
    
}
