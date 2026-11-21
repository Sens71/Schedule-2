using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ReagentSlots : MonoBehaviour
{
    public VisualTreeAsset ghostTemplate;

    private VisualElement doc;
    public ReagentData startReagent;
    private VisualElement ghost;

    private List<VisualElement> slots = new();

    private void OnEnable()
    {
        doc = GetComponent<UIDocument>().rootVisualElement;
        VisualElement ghost = doc.Q<VisualElement>("Ghost");
        
        slots.Clear();
        slots.Add(doc.Q<VisualElement>("Slot1"));
        slots.Add(doc.Q<VisualElement>("Slot2"));
        slots.Add(doc.Q<VisualElement>("Slot3"));

        foreach (VisualElement slot in slots)
        {
            slot.RegisterCallback<PointerUpEvent>(OnUp);
            slot.RegisterCallback<PointerDownEvent>(OnDown);
            slot.RegisterCallback<PointerMoveEvent>(OnMove);
        }
        
        Show(slots[0], startReagent);
        
        Drag.Released += OnReleased;
    }

    private void OnDisable()
    {
        Drag.Released -= OnReleased;
    }


    private void OnDown(PointerDownEvent evt)
    {
        VisualElement source = (VisualElement)evt.currentTarget;
        if(source.userData == null)
            return;
        
        source.CapturePointer(evt.pointerId);
        if(source.userData is not ReagentData reagent)
            return;
        
        ghost = ghostTemplate.Instantiate().Q<VisualElement>("Ghost");
        VisualElement icon = ghost.Q<VisualElement>("Icon");
        icon.style.backgroundImage = new StyleBackground(reagent.icon);
        doc.Add(ghost);
        
        
    }

    private void OnMove(PointerMoveEvent evt)
    {
        if (ghost == null)
            return;
        
        ghost.style.left = evt.position.x;
        ghost.style.top = evt.position.y;
    }
    
    private void OnUp(PointerUpEvent evt)
    {
        VisualElement source = (VisualElement)evt.currentTarget;
        if(!source.HasPointerCapture(evt.pointerId))
            return;
        
        source.ReleasePointer(evt.pointerId);
        ghost.RemoveFromHierarchy();
        ghost = null;
        Drag.Release(source, evt.position);
    }
    
    private void OnReleased(VisualElement source, Vector2 position)
    {
        VisualElement target = source.panel.Pick(position);
        if(!slots.Contains(target) || target == source)
            return;
        
        if(source.userData is not ReagentData reagent)
            return;
        
        ReagentData old = target.userData as ReagentData;
        Show(target, reagent);
        Show(source, old);
    }
    
    
    private void Show(VisualElement slot, ReagentData reagent)
    {
        slot.userData = reagent;
        
        VisualElement icon = slot.Q<VisualElement>("Icon");
        Label name = slot.Q<Label>("Name");

        if (reagent == null)
        {
            icon.style.backgroundImage = StyleKeyword.Null;
            name.text = "-";
            return;
        }

        icon.style.backgroundImage = new StyleBackground(reagent.icon);
        name.text = reagent.name;
    }
    
    private void SetFrame(bool highlight)
    {
        Color color = highlight ? Color.yellow : Color.white;
        foreach (VisualElement slot in slots)
        {
            slot.style.borderTopColor = color;
            slot.style.borderBottomColor = color;
            slot.style.borderLeftColor = color;
            slot.style.borderRightColor = color;
        }
        
    }
}
