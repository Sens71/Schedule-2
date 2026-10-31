using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DropSource : MonoBehaviour
{
    private VisualElement doc;
    private Label log;
    
    private VisualElement targetA;
    private VisualElement targetB;
    private VisualElement targetC;
    
    private void OnEnable()
    {
        doc = GetComponent<UIDocument>().rootVisualElement;
        log = doc.Q<Label>("Log");
        
        targetA = doc.Q<VisualElement>("TargetA");  
        targetB = doc.Q<VisualElement>("TargetB");
        targetC = doc.Q<VisualElement>("TargetC");

        targetA.RegisterCallback<PointerDownEvent>(OnDown);
        targetB.RegisterCallback<PointerDownEvent>(OnDown);
        targetC.RegisterCallback<PointerDownEvent>(OnDown);
        
        targetA.RegisterCallback<PointerUpEvent>(OnUp);
        targetB.RegisterCallback<PointerUpEvent>(OnUp);
        targetC.RegisterCallback<PointerUpEvent>(OnUp);

        targetA.userData = 1;
        targetB.userData = 2;
        targetC.userData = 3;
    }

    private void OnDown(PointerDownEvent evt)
    {
        VisualElement target = (VisualElement)evt.currentTarget;
        target.CapturePointer(evt.pointerId);
    }

    private void OnUp(PointerUpEvent evt)
    {
        VisualElement target = (VisualElement)evt.currentTarget;
        target.ReleasePointer(evt.pointerId);
        Drag.Release((VisualElement)evt.currentTarget, evt.position);
    }
}
