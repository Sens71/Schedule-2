using UnityEngine;
using UnityEngine.EventSystems;

public class HotKeyDrop : MonoBehaviour,IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        print(gameObject.name);
    }
}
