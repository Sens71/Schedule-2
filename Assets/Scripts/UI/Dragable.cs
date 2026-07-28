using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject ghost;
    public ItemData item;
    [SerializeField] private TMP_Text textAmount;
    [SerializeField] private TMP_Text textName;
    [SerializeField] private Image icon;
    [SerializeField] private Image bg;
    
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

    public void SetItem(ItemData itemData)
    {
        this.item = itemData;
        textAmount.text = itemData.amount.ToString();
        textName.text = itemData.name;
        icon.sprite = itemData.icon;
        bg.color = itemData.bgColor;
    }
}
