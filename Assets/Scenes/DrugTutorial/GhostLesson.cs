using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GhostLesson : MonoBehaviour
{
    private VisualElement ghost;
    private bool isDragging = false;

    private void OnEnable()
    {
        VisualElement doc = GetComponent<UIDocument>().rootVisualElement;
        VisualElement captured = doc.Q<VisualElement>("Box");
        VisualElement root = doc.Q<VisualElement>("root");
        ghost = doc.Q<VisualElement>("Ghost");
        
        captured.RegisterCallback<PointerMoveEvent>(OnMove);
        captured.RegisterCallback<PointerUpEvent>(OnUp);
        captured.RegisterCallback<PointerDownEvent>(OnDown);
    }

    private void OnMove(PointerMoveEvent evt)
    {
        if (isDragging == false)
         return;
        ghost.style.left = evt.position.x;
        ghost.style.top = evt.position.y;
    }

    private void OnDown(PointerDownEvent evt)
    {
        VisualElement element = (VisualElement)evt.currentTarget;
        element.CapturePointer(evt.pointerId);
        isDragging = true;
    }

    private void OnUp(PointerUpEvent evt)
    {
        VisualElement element = (VisualElement)evt.currentTarget;
        element.ReleasePointer(evt.pointerId);
        isDragging = false;
    }
    
}
