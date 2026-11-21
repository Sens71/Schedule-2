using System;
using UnityEngine;
using UnityEngine.UIElements;

public class StringReciever : MonoBehaviour
{
    private VisualElement box;
    private Label value;

    private void OnEnable()
    {
        VisualElement doc = GetComponent<UIDocument>().rootVisualElement;
        box = doc.Q<VisualElement>("StringBox");
        value = box.Q<Label>("Value");

        Drag.Grabbed += OnGrabbed;
        Drag.Released += OnReleased;
    }

    private void OnDisable()
    {
        Drag.Grabbed -= OnGrabbed;
        Drag.Released -= OnReleased;
    }

    private void OnGrabbed(VisualElement source)
    {
        SetFrame(source.userData is string);
    }

    private void OnReleased(VisualElement source, Vector2 position)
    {
        SetFrame(false);

        if (box.panel.Pick(position) != box)
            return;

        if (source.userData is string text)
            value.text = text;

    }

    private void SetFrame(bool highlight)
    {
        Color color = highlight ? Color.yellow : Color.white;
        box.style.borderTopColor = color;
        box.style.borderBottomColor = color;
        box.style.borderLeftColor = color;
        box.style.borderRightColor = color;
    }
}
