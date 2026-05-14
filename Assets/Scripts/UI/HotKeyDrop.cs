using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HotKeyDrop : MonoBehaviour,IDropHandler
{
    public ItemData item;
    public Image icon;
    public void OnDrop(PointerEventData eventData)
    {
        var dragable = eventData.pointerDrag.GetComponent<Dragable>();
        item = dragable.item;
        icon.sprite = item.icon;
        icon.color = Color.white;
    }
}
