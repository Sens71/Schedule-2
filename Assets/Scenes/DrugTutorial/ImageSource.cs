using UnityEngine;
using UnityEngine.UIElements;

public class ImageSource : MonoBehaviour
{
    private VisualElement doc;
    private VisualElement icon1;
    private VisualElement icon2;
    private void OnEnable()
    {
        doc = GetComponent<UIDocument>().rootVisualElement;
        icon1 = doc.Q<VisualElement>("Icon1");
        icon2 = doc.Q<VisualElement>("Icon2");
        
        icon1.RegisterCallback<PointerDownEvent>(OnDown);
        icon2.RegisterCallback<PointerDownEvent>(OnDown);
        
        
        icon1.RegisterCallback<PointerUpEvent>(OnUp);
        icon2.RegisterCallback<PointerUpEvent>(OnUp);
    }

    private void OnDown(PointerDownEvent evt)
    {
        VisualElement target = (VisualElement)evt.currentTarget;
        
        target.userData = target.style.backgroundImage;
        target.CapturePointer(evt.pointerId);
    }

    private void OnUp(PointerUpEvent evt)
    { 
        VisualElement target = (VisualElement)evt.currentTarget;
        
        target.ReleasePointer(evt.pointerId);
        Drag.Release(target, evt.position); 
    }
}
