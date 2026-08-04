using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject ghost;
    public ItemData item;
    public Drug drug;
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

    private void OnDestroy()
    {
        if (ghost == null)
            return;
        Destroy(ghost);
        DragEnded?.Invoke();
    }

    public void SetItem(ItemData itemData)
    {
        this.item = itemData;
        this.drug = null;
        textAmount.text = itemData.amount.ToString();
        textName.text = itemData.name;
        icon.sprite = itemData.icon;
        icon.color = Color.white;
        bg.color = itemData.bgColor;
    }

    /// <summary>
    /// Показать намиксованный наркотик. Иконка белая — её красит уникальный
    /// цвет состава, посчитанный в <see cref="Drug"/>.
    /// </summary>
    public void SetDrug(Drug drug)
    {
        this.item = null;
        this.drug = drug;
        textAmount.text = drug.amount.ToString();
        textName.text = drug.name;
        icon.sprite = drug.icon;
        icon.color = drug.iconColor;
    }
}
