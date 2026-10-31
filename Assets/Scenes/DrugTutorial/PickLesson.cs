using UnityEngine;
using UnityEngine.UIElements;

public class PickLesson : MonoBehaviour
{
    private VisualElement screen;
    private Label log;

    private void OnEnable()
    {
        VisualElement doc = GetComponent<UIDocument>().rootVisualElement;
        screen = doc.Q<VisualElement>("root");
        log = doc.Q<Label>("Log");
        
        screen.RegisterCallback<PointerDownEvent>(OnDown);
    }

    private void OnDown(PointerDownEvent evt)
    {
        VisualElement picked = screen.panel.Pick(evt.position);

        if (picked == null)
        {
            log.text = "Pick: null";
            return;
        }
        
        log.text = "Pick: " + picked.name + " parent : " + picked.parent?.name;
    }
}
