using System;
using UnityEngine;
using UnityEngine.UIElements;

public class DropHandler : MonoBehaviour
{
    private VisualElement doc;
    private Label result;
    private VisualElement box;
    private int counter;
    
    private void OnEnable()
    { 
        doc = GetComponent<UIDocument>().rootVisualElement;
        result = doc.Q<Label>("Result");
        box = doc.Q<VisualElement>("Box");
        
        Drag.Released += OnUp;
    }

    private void OnUp(VisualElement ve, Vector2 position)
    {
        if(doc.panel.Pick(position) != box)
            return;
        if (ve.userData is float || ve.userData is int)
        {
            result.text = ve.userData.ToString();
        }
        else if (ve.userData is StyleBackground)
        {
            box.style.backgroundImage = (StyleBackground)ve.userData;
        }

    }
    
}
