using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MixerSlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public Image icon;
    private Image border;
    public Color borderNormal = Color.white;
    public Color borderHighlighted = Color.yellow;
    public TMP_Text amountText;

    public event Action<MixerSlot, ReagentData> OnDropped;
    public event Action<MixerSlot> OnTakeRequested;
    private void Awake()
    {
        border = transform.Find("Border")?.GetComponent<Image>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Dragable dragable = eventData.pointerDrag.GetComponent<Dragable>();
        if (dragable == null)
            return;

        ReagentData reagent = dragable.item as ReagentData;
        if (reagent == null)
            return;

        OnDropped?.Invoke(this, reagent);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            OnTakeRequested?.Invoke(this);
    }

    public void SetHighlight(bool on)
    {
        border.color = on ? borderHighlighted : borderNormal;
    }

    public void Show(SlotContent content)
    {
        icon.sprite = !content.IsEmpty ? content.item.icon : null;
        icon.color = !content.IsEmpty ? Color.white : Color.clear;
        amountText.text = !content.IsEmpty ? content.count.ToString() : "";
    }
}
