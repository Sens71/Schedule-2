using System;
using UnityEngine;
using UnityEngine.UIElements;

public class DropHandler : MonoBehaviour
{
    private VisualElement doc;
    private Label result;
    private VisualElement box;
    private VisualElement box2;
    private int counter;
    
    private void OnEnable()
    { 
        doc = GetComponent<UIDocument>().rootVisualElement;
        result = doc.Q<Label>("Result");
        box = doc.Q<VisualElement>("Box");
        box2 = doc.Q<VisualElement>("Box2");
        
        Drag.Released += OnUp;
    }

    private void OnUp(VisualElement ve, Vector2 position)
    {
        if (doc.panel.Pick(position) == box)
        {
            if (ve.userData is float || ve.userData is int)
            {
                result.text = ve.userData.ToString();
            }
            else if (ve.userData is Background)
            {
                Background bg = (Background)ve.userData;
                box.style.backgroundImage = new StyleBackground(bg);
            }
        }
        else if (doc.panel.Pick(position) == box2)
        {
           box2.style.backgroundColor = ve.resolvedStyle.backgroundColor;
        }
            
        

    }
    
}
