using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Ячейка миксера — приёмник предметов, которые тащат из инвентаря
/// (см. <see cref="Dragable"/>). Принимает только предметы из белого списка
/// <see cref="allowedItems"/>. Если список пуст — принимает любой предмет
/// (для слотов Side Ingredients).
///
/// Поведение при дропе:
///  - пустой валидный слот      -> кладём предмет, count = 1, инвентарь -1;
///  - тот же предмет            -> count += 1, инвентарь -1;
///  - другой предмет из списка  -> замена: старый стек возвращается в инвентарь,
///                                 новый предмет кладётся, инвентарь -1;
///  - предмет не из списка      -> отказ, ничего не меняется.
/// </summary>
public class MixerSlot : MonoBehaviour, IDropHandler
{
    public Image icon;
    public Color borderNormal = Color.white;
    public Color borderHighlighted = Color.yellow;
    public TMP_Text amountText;
    
    public event Action<MixerSlot> OnChanged;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        Dragable.DragStarted += OnDragStarted;
        Dragable.DragEnded += OnDragEnded;
    }

    private void OnDisable()
    {
        Dragable.DragStarted -= OnDragStarted;
        Dragable.DragEnded -= OnDragEnded;
    }

    public void OnDragStarted(ItemData dragged)
    {

    }

    public void OnDragEnded()
    {
        
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        
    }

    /// <summary>Проверка, разрешён ли предмет в этом слоте.</summary>
    public bool Accepts(ReagentData candidate)
    {
        return false;
    }

    
}
