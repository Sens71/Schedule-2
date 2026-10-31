using System;
using UnityEngine;
using UnityEngine.UIElements;

public class DropScript : MonoBehaviour
{
    private VisualElement doc;
    private VisualElement box2;
    private void OnEnable()
    {
        doc = GetComponent<UIDocument>().rootVisualElement;
        box2 = doc.Q<VisualElement>("TimeBox");
        
        box2.RegisterCallback<PointerDownEvent>(OnDown);
        box2.RegisterCallback<PointerUpEvent>(OnUp);
    }

    private void OnDown(PointerDownEvent evt)
    {
        box2.userData = Time.time;
        box2.CapturePointer(evt.pointerId);
    }

    private void OnUp(PointerUpEvent evt)
    {
        box2.ReleasePointer(evt.pointerId);
        Drag.Release((VisualElement)evt.currentTarget, evt.position);
    }
}
