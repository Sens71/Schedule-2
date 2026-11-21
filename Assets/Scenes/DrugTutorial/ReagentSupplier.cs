using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ReagentSupplier : MonoBehaviour
{
    public VisualTreeAsset ghostTemplate;
    
    private List<VisualElement> elementSlots = new();
    public List<ReagentData> data = new();
    private VisualElement supplier;
    private VisualElement doc;
    private VisualElement ghost;

    private void OnEnable()
    {
        doc = GetComponent<UIDocument>().rootVisualElement;
        elementSlots.Clear();
        supplier = doc.Q<VisualElement>("Supplier");
        elementSlots.Add(supplier.Q<VisualElement>("Slot1"));
        elementSlots.Add(supplier.Q<VisualElement>("Slot2"));
        elementSlots.Add(supplier.Q<VisualElement>("Slot3"));
        int i = 0;
        foreach (var element in elementSlots)
        {
            VisualElement icon = element.Q<VisualElement>("Icon");
            Label name =  element.Q<Label>("Name");
            icon.style.backgroundImage = new StyleBackground(data[i].icon);
            name.text = data[i].name;
            element.userData = data[i];
            element.RegisterCallback<PointerDownEvent>(OnDown);
            element.RegisterCallback<PointerUpEvent>(OnUp);
            element.RegisterCallback<PointerMoveEvent>(OnMove);
            i++;
        }
        
        
    }

    private void OnDown(PointerDownEvent evt)
    {
        VisualElement source = (VisualElement)evt.currentTarget;
        if(source.userData is not ReagentData reagent)
            return;
        source.CapturePointer(evt.pointerId);
        
        ghost = ghostTemplate.Instantiate().Q<VisualElement>("Ghost");
        ghost.style.left = evt.position.x;
        ghost.style.top = evt.position.y;
        VisualElement icon = ghost.Q<VisualElement>("Icon");
        icon.style.backgroundImage = new StyleBackground(reagent.icon);
        doc.Add(ghost);
    }

    private void OnUp(PointerUpEvent evt)
    {
        VisualElement source = (VisualElement)evt.currentTarget;
        if(!source.HasPointerCapture(evt.pointerId))
            return;
        
        source.ReleasePointer(evt.pointerId);
        ghost.RemoveFromHierarchy();
        ghost = null;
        Drag.Release(source,evt.position);
    }

    private void OnMove(PointerMoveEvent evt)
    {
        if (ghost == null)
            return;
        
        ghost.style.left = evt.position.x;
        ghost.style.top = evt.position.y;
    }
    
}
