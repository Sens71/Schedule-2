using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Template : MonoBehaviour
{
    public VisualTreeAsset ghostTemplate;
    
    private VisualElement doc;
    private VisualElement box;
    private VisualElement ghost;
    private Label log;
    private void OnEnable()
    {
        doc = GetComponent<UIDocument>().rootVisualElement;
        box = doc.Q<VisualElement>("Box");
        log = doc.Q<Label>("Log");
        
        box.RegisterCallback<PointerDownEvent>(OnDown);
        box.RegisterCallback<PointerUpEvent>(OnUp);
        box.RegisterCallback<PointerMoveEvent>(OnMove);
    }

    private void OnDown(PointerDownEvent evt)
    {
        box.CapturePointer(evt.pointerId);
        
        ghost = ghostTemplate.Instantiate().Q<VisualElement>("Ghost");
        doc.Add(ghost);
        
        ghost.style.left = evt.position.x;
        ghost.style.top = evt.position.y;

        log.text = "Ghost created.Element in doc :" + doc.childCount;
    }

    public void OnMove(PointerMoveEvent evt)
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
        
        log.text = "Ghost remove. Element in doc :" + doc.childCount;
    }
}
