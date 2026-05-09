using UnityEngine;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject ghost;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(ghost != null)
            return;
        ghost = Instantiate(gameObject, transform.position, Quaternion.identity,transform.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(ghost == null)
            return;
        ghost.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(ghost == null)
            return;
        Destroy(ghost);
    }
}
