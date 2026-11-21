using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GrabSupplies : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement doc = GetComponent<UIDocument>().rootVisualElement;
        VisualElement num1 = doc.Q<VisualElement>("Num1");
        VisualElement num2 = doc.Q<VisualElement>("Num2");
        VisualElement str1 = doc.Q<VisualElement>("Str1");
        VisualElement str2 = doc.Q<VisualElement>("Str2");

        num1.userData = 1;
        num2.userData = 2;
        str1.userData = "Apple";
        str2.userData = "Pear";
        
        num1.RegisterCallback<PointerUpEvent>(OnUp);
        num1.RegisterCallback<PointerDownEvent>(OnDown);
        num2.RegisterCallback<PointerUpEvent>(OnUp);
        num2.RegisterCallback<PointerDownEvent>(OnDown);
        str1.RegisterCallback<PointerUpEvent>(OnUp);
        str1.RegisterCallback<PointerDownEvent>(OnDown);
        str2.RegisterCallback<PointerUpEvent>(OnUp);
        str2.RegisterCallback<PointerDownEvent>(OnDown);
        
    }
    
    private void OnDown(PointerDownEvent evt)
    {
        VisualElement source = (VisualElement)evt.currentTarget;
        source.CapturePointer(evt.pointerId);
        
        Drag.Grab(source);
    }
    
    private void OnUp(PointerUpEvent evt)
    {
        VisualElement source = (VisualElement)evt.currentTarget;
        if(!source.HasPointerCapture(evt.pointerId))
            return;
        
        source.ReleasePointer(evt.pointerId);
        
        Drag.Release(source, evt.position);
    }

    
}

