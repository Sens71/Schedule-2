using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DropLesson : MonoBehaviour
{
    public VisualTreeAsset ghostTemplate;

    private VisualElement doc;
    private VisualElement box;
    private VisualElement ghost;
    private Label log;
    private VisualElement targetA;
    private VisualElement targetB;
    private VisualElement targetC;

    private void OnEnable()
    {
        doc = GetComponent<UIDocument>().rootVisualElement;
        box = doc.Q<VisualElement>("Box");
        log = doc.Q<Label>("Log");
        
        targetA = doc.Q<VisualElement>("TargetA");
        targetB = doc.Q<VisualElement>("TargetB");
        targetC = doc.Q<VisualElement>("TargetC");
        
        box.RegisterCallback<PointerUpEvent>(OnUp);
        box.RegisterCallback<PointerDownEvent>(OnDown);
        box.RegisterCallback<PointerMoveEvent>(OnMove);
    }

    private void OnDown(PointerDownEvent evt)
    {
        box.CapturePointer(evt.pointerId);
        
        ghost = ghostTemplate.Instantiate().Q<VisualElement>("Ghost");
        doc.Add(ghost);
        
        ghost.style.left = evt.position.x;
        ghost.style.top = evt.position.y;
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
        if (ghost == null)
            return;
        
        box.ReleasePointer(evt.pointerId);
        
        ghost.RemoveFromHierarchy();
        ghost = null;

        VisualElement picked = doc.panel.Pick(evt.position);

        if (picked == targetA || picked == targetB || picked == targetC)
        {
            log.text = "Dropped on" + picked.name;
            Color color = picked.style.backgroundColor.value;
            Color newColor = color == Color.red ? Color.green : Color.red;
            picked.style.backgroundColor = new StyleColor(newColor);
            
        }

        log.text = "Dropped on" + picked.name;
    }

    
    
    
}
