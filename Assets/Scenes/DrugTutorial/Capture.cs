using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Capture : MonoBehaviour
{
    private Label capturedLog;
    private int capturedMoves;

    private void OnEnable()
    {
        VisualElement doc = GetComponent<UIDocument>().rootVisualElement;
        VisualElement captured = doc.Q<VisualElement>("Box");
        VisualElement captured1 = doc.Q<VisualElement>("Box2");
        capturedLog = doc.Q<Label>("Log");
        
        
        captured.RegisterCallback<PointerUpEvent>(OnUp);
        captured.RegisterCallback<PointerDownEvent>(OnDown);
        captured.RegisterCallback<PointerMoveEvent>(OnMove);
        
        captured1.RegisterCallback<PointerUpEvent>(OnUp);
        captured1.RegisterCallback<PointerDownEvent>(OnDown);
        captured1.RegisterCallback<PointerMoveEvent>(OnMove);
    }

    private void OnDown(PointerDownEvent evt)
    {
        VisualElement element = (VisualElement)evt.currentTarget;
        element.CapturePointer(evt.pointerId);

        capturedMoves = 0;
        capturedLog.text = "Captured: down";
    }

    private void OnUp(PointerUpEvent evt)
    {
        VisualElement element = (VisualElement)evt.currentTarget;
        element.ReleasePointer(evt.pointerId);
        
        capturedLog.text = "Captured: down,moves"+ capturedMoves +" UP" ;
    }

    private void OnMove(PointerMoveEvent evt)
    {
        capturedMoves++;
        VisualElement element = (VisualElement)evt.currentTarget;
        capturedLog.text = "Captured: down, moves" + capturedMoves + ". " + element.name;
    }
}
