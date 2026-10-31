using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Pointer : MonoBehaviour
{   
    private VisualElement box;
    private Label log;
    private int moves;
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        box = root.Q<VisualElement>("Box");
        log = root.Q<Label>("Log");
        
        box.RegisterCallback<PointerDownEvent>(OnDown);
        box.RegisterCallback<PointerMoveEvent>(OnMove);
        box.RegisterCallback<PointerUpEvent>(OnUp);
    }

    private void OnDown(PointerDownEvent evt)
    {
        moves = 0;
        log.text = "Down button: " + evt.button + "  position: " + evt.position;
    }

    private void OnMove(PointerMoveEvent evt)
    {
        moves++;
        log.text = "Move count: " + moves + "  position: " + evt.position;
    }

    private void OnUp(PointerUpEvent evt)
    {
        log.text = "Up button: " + evt.button + "  moves: " + moves;
    }
}
