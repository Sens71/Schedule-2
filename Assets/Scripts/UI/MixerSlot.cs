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
    [Header("Фильтр")]
    [Tooltip("Какие предметы принимает слот. Пусто = принимать любой предмет.")]
    public List<ReagentData> allowedItems = new();

    [Header("Визуал")]
    [Tooltip("Image с иконкой предмета (дочерний объект 'Icon').")]
    public Image icon;

    public Image border;
    public Color borderNormal = Color.white;
    public Color borderHighlighted = Color.yellow;
    [Tooltip("Текст с количеством. Необязательно.")]
    public TMP_Text amountText;

    [Tooltip("Прятать текст количества, когда в слоте <= 1 предмета.")]
    public bool hideAmountWhenSingle = true;

    /// <summary>Предмет, лежащий в слоте (null — слот пуст).</summary>
    public ReagentData PlacedItem { get; private set; }

    /// <summary>Сколько единиц предмета сейчас в слоте.</summary>
    public int Count { get; private set; }

    /// <summary>Вызывается при любом изменении содержимого слота.</summary>
    public event Action<MixerSlot> OnChanged;

    private void Awake()
    {
        border = transform.Find("Border")?.GetComponent<Image>();
        RefreshVisual();
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
        border.color = Accepts(dragged as ReagentData) ? borderHighlighted : borderNormal;
    }

    public void OnDragEnded()
    {
        border.color = borderNormal;
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        var dragable = eventData.pointerDrag.GetComponent<Dragable>();
        if (dragable == null)
            return;

        TryPlace(dragable.item as ReagentData);
    }

    /// <summary>Проверка, разрешён ли предмет в этом слоте.</summary>
    public bool Accepts(ReagentData candidate)
    {
        if (candidate == null)
            return false;
        if (allowedItems == null || allowedItems.Count == 0)
            return true;
        return allowedItems.Contains(candidate);
    }

    /// <summary>
    /// Попытаться положить предмет в слот. Возвращает true, если предмет принят.
    /// Сам списывает 1 из инвентаря и возвращает старый стек при замене.
    /// </summary>
    public bool TryPlace(ReagentData item)
    {
        if (item == null || !Accepts(item))
            return false;

        // В инвентаре не осталось предметов — брать нечего.
        if (item.amount <= 0)
            return false;

        if (PlacedItem == null)
        {
            // Пустой слот.
            PlacedItem = item;
            Count = 1;
            item.ChangeAmount(-1);
        }
        else if (PlacedItem == item)
        {
            // Тот же предмет — увеличиваем стек.
            Count++;
            item.ChangeAmount(-1);
        }
        else
        {
            // Другой предмет из белого списка — замена.
            ReturnToInventory();
            PlacedItem = item;
            Count = 1;
            item.ChangeAmount(-1);
        }

        RefreshVisual();
        OnChanged?.Invoke(this);
        return true;
    }

    /// <summary>Полностью очистить слот, вернув содержимое в инвентарь.</summary>
    public void Clear()
    {
        if (PlacedItem == null)
            return;

        ReturnToInventory();
        RefreshVisual();
        OnChanged?.Invoke(this);
    }

    /// <summary>Опустошить слот без возврата в инвентарь — реагенты израсходованы в миксе.</summary>
    public void Consume()
    {
        if (PlacedItem == null)
            return;

        PlacedItem = null;
        Count = 0;
        RefreshVisual();
        OnChanged?.Invoke(this);
    }

    private void ReturnToInventory()
    {
        if (PlacedItem != null && Count > 0)
            PlacedItem.ChangeAmount(Count);
        PlacedItem = null;
        Count = 0;
    }

    private void RefreshVisual()
    {
        var hasItem = PlacedItem != null && Count > 0;

        if (icon != null)
        {
            icon.sprite = hasItem ? PlacedItem.icon : null;
            icon.color = hasItem ? Color.white : Color.clear;
            icon.enabled = hasItem;
        }

        if (amountText != null)
        {
            var showAmount = hasItem && !(hideAmountWhenSingle && Count <= 1);
            amountText.text = showAmount ? Count.ToString() : string.Empty;
            amountText.enabled = showAmount;
        }
    }
}
