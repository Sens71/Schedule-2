using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Drag
{
    public static event Action<VisualElement> Grabbed;
    public static event Action<VisualElement, Vector2> Released;

    public static void Grab(VisualElement source)
    {
        Grabbed?.Invoke(source);
    }

    public static void Release(VisualElement source, Vector2 position)
    {
        Released?.Invoke(source, position);
    }
}
