using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class SlotFilter
{
    public List<ReagentData> items = new();
}
public class MixerWindow : MonoBehaviour, IUIPanel
{
    public Storage storage;
    public VisualTreeAsset slotTemplate;
    public VisualTreeAsset cardTemplate;
    public float interactDistance = 1.5f;

    public List<SlotFilter> whiteList = new();
    
    private VisualElement root;
    private VisualElement window;
    private VisualElement mainSlotsBox;
    private VisualElement sideSlotsBox;
    private VisualElement itemsBox;
    private VisualElement resultIcon;
    private Label resultAmount;
    private Label timerLabel;
    private Button mixButton;
    private Button cashButton;
    private Button closeButton;

    private VisualElement ghost;
    private VisualElement ghostIcon;
    private ReagentData draggedReagent;
    
    private List<VisualElement> slotElements = new();
    private List<VisualElement> cards = new();
    private List<ReagentData> cardReagents = new();

    private Mixer currentMixer;
    private bool isOpen;
    private PlayerInputActions playerInputActions;

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        root.pickingMode = PickingMode.Ignore;

        window = root.Q<VisualElement>("Window");
        mainSlotsBox = root.Q<VisualElement>("MainSlots");
        sideSlotsBox = root.Q<VisualElement>("SideSlots");
        itemsBox = root.Q<VisualElement>("Items");
        resultIcon = root.Q<VisualElement>("ResultIcon");
        resultAmount = root.Q<Label>("ResultAmount");
        timerLabel = root.Q<Label>("Timer");
        mixButton = root.Q<Button>("MixButton");
        cashButton = root.Q<Button>("CashButton");
        closeButton = root.Q<Button>("CloseButton");
        ghost = root.Q<VisualElement>("Ghost");
        ghostIcon = root.Q<VisualElement>("GhostIcon");
        
        slotElements.Clear();
        BuildSlots(mainSlotsBox, 0,Mixer.MainCount);
        BuildSlots(sideSlotsBox, Mixer.MainCount, Mixer.SlotCount);
        BuildCards();

        mixButton.clicked += OnMix;
        cashButton.clicked += OnCash;
        closeButton.clicked += Close;

        ghost.style.display = DisplayStyle.None;
        window.style.display = DisplayStyle.None;
    }

    private void OnDisable()
    {
        mixButton.clicked -= OnMix;
        cashButton.clicked -= OnCash;
        closeButton.clicked -= Close;
    }

    private void Start()
    {
        playerInputActions = Player.Instance.inputActions;
    }

    private void Update()
    {
        if (isOpen)
        {
            if (currentMixer.QueueCount > 0)
            {
                timerLabel.text = currentMixer.TimeLeft().ToString();
            }
            else
            {
                timerLabel.text = "";
            }

            ShowResult();
            return;
        }

        if (!playerInputActions.PlayerControl.Interact.WasPressedThisFrame())
            return;

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance)
            && hit.collider.TryGetComponent(out Mixer mixer))
        {
            currentMixer = mixer;
            currentMixer.OnChanged += Refresh;
            Open();
        }
    }

    public void Open()
    {
        if (isOpen)
            return;

        isOpen = true;
        window.style.display = DisplayStyle.Flex;
        Refresh();
        IUIPanel.Notify(this, true, true);
    }

    public void Close()
    {
        if (!isOpen)
            return;

        isOpen = false;
        EndDrag();

        currentMixer.OnChanged -= Refresh;
        currentMixer = null;

        window.style.display = DisplayStyle.None;
        IUIPanel.Notify(this, false, false);
    }

    public bool Accepts(int index, ReagentData item)
    {
        if (index >= whiteList.Count || whiteList[index].items.Count == 0)
            return true;
        
        return  whiteList[index].items.Contains(item);
    }
    
    private void BuildSlots(VisualElement box, int from, int to)
    {
        box.Clear();

        for (int i = 0; i < to; i++)
        {
            slotTemplate.CloneTree(box);
            VisualElement slot = box[box.childCount - 1];

            slot.userData = i;
            slot.RegisterCallback<PointerDownEvent>(OnSlotPointerDown);
            slotElements.Add(slot);
        }
    }

    private void BuildCards()
    {
        itemsBox.Clear();
        cards.Clear();
        cardReagents.Clear();

        foreach (ItemData item in storage.items)
        {
            ReagentData reagent = item as ReagentData;
            if (reagent == null)
                continue;

            cardTemplate.CloneTree(itemsBox);
            VisualElement card = itemsBox[cards.Count];

            card.userData = reagent;
            card.Q<Label>("Title").text = reagent.name;
            SetIcon(card.Q<VisualElement>("Icon"), reagent.icon);

            card.RegisterCallback<PointerDownEvent>(OnCardPointerDown);
            card.RegisterCallback<PointerMoveEvent>(OnCardPointerMove);
            card.RegisterCallback<PointerUpEvent>(OnCardPointerUp);

            cards.Add(card);
            cardReagents.Add(reagent);
        }
    }

    private void OnCardPointerDown(PointerDownEvent evt)
    {
        if (evt.button != 0)
            return;

        VisualElement card = (VisualElement)evt.currentTarget;
        draggedReagent = (ReagentData)card.userData;

        card.CapturePointer(evt.pointerId);
        SetIcon(ghostIcon, draggedReagent.icon);
        ghost.style.display = DisplayStyle.Flex;
        MoveGhost(evt.position);

        HighlightSlots();
    }

    private void OnCardPointerMove(PointerMoveEvent evt)
    {
        if (draggedReagent == null)
            return;

        MoveGhost(evt.position);
    }

    private void OnCardPointerUp(PointerUpEvent evt)
    {
        if (draggedReagent == null)
            return;

        VisualElement card = (VisualElement)evt.currentTarget;
        card.ReleasePointer(evt.pointerId);

        VisualElement target = root.panel.Pick(evt.position);
        if(target != null && target.userData is int index && Accepts(index,draggedReagent))
            currentMixer.Place(index, draggedReagent);
        
        EndDrag();
    }

    private void OnSlotPointerDown(PointerDownEvent evt)
    {
        if (evt.button != 1)
            return;

        VisualElement slot = (VisualElement)evt.currentTarget;
        currentMixer.Take((int)slot.userData);
    }

    private void MoveGhost(Vector3 position)
    {
        ghost.style.left = position.x - 24f;
        ghost.style.top = position.y - 24f;
    }

    private void EndDrag()
    {
        draggedReagent = null;
        ghost.style.display = DisplayStyle.None;
        
        foreach (VisualElement slot in slotElements)
            SetHighlight(slot, true, true);
    }

    private void HighlightSlots()
    {
        for (int i = 0; i < slotElements.Count; i++)
            SetHighlight(slotElements[i], Accepts(i, draggedReagent), false);
    }

    private static void SetHighlight(VisualElement slot, bool accepted, bool idle)
    {
        slot.EnableInClassList("slot--accept", !idle && accepted);
        slot.EnableInClassList("slot--reject", !idle && !accepted);
    }

    private static void SetIcon(VisualElement icon, Sprite sprite)
    {
        if (sprite == null)
        {
            icon.style.display = DisplayStyle.None;
            return;
        }

        icon.style.display = DisplayStyle.Flex;
        icon.style.backgroundImage = new StyleBackground(sprite);
    }

    private void Refresh()
    {
        for (int i = 0; i < slotElements.Count; i++)
            ShowSlot(slotElements[i], currentMixer.slots[i]);

        for (int i = 0; i < cards.Count; i++)
        {
            ReagentData reagent = cardReagents[i];
            cards[i].Q<Label>("Amount").text = reagent.amount.ToString();

            if (reagent.amount > 0)
            {
                cards[i].style.display = DisplayStyle.Flex;
            }
            else
            {
                cards[i].style.display = DisplayStyle.None;
            }
        }

        ShowResult();
    }

    private static void ShowSlot(VisualElement slot, SlotContent content)
    {
        VisualElement icon = slot.Q<VisualElement>("Icon");
        Label count = slot.Q<Label>("Count");

        if (content.IsEmpty)
        {
            SetIcon(icon, null);
            count.text = "";
            return;
        }

        SetIcon(icon, content.item.icon);
        count.text = content.count.ToString();
    }

    private void ShowResult()
    {
        List<Drug> ready = currentMixer.ready;

        if (ready.Count == 0)
        {
            SetIcon(resultIcon, null);
            resultAmount.text = "";
            cashButton.SetEnabled(false);
            return;
        }

        SetIcon(resultIcon, ready[0].icon);
        resultIcon.style.unityBackgroundImageTintColor = ready[0].iconColor;
        resultAmount.text = ready.Count.ToString();
        cashButton.SetEnabled(true);
    }

    private void OnMix()
    {
        currentMixer.Mix();
    }

    private void OnCash()
    {
        currentMixer.CashResult();
    }
}
