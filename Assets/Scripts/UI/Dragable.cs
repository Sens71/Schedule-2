using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject ghost;
    public ItemData item;
    
    public static event Action<ItemData> DragStarted;
    public static event Action DragEnded;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(ghost != null)
            return;
        
        var root = GetComponentInParent<Canvas>().rootCanvas;
        ghost = Instantiate(gameObject, transform.position, Quaternion.identity,root.transform);
        ghost.transform.SetAsLastSibling();
        ghost.AddComponent<CanvasGroup>().blocksRaycasts = false;
        Destroy(ghost.GetComponent<Dragable>());
        DragStarted?.Invoke(item);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(ghost == null)
            return;
        ghost.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {   
        DragEnded?.Invoke();
        if(ghost == null)
            return;
        Destroy(ghost);
    }
}
