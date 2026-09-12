using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MixerPresenter : MonoBehaviour, IUIPanel
{
    public List<MixerSlot> mainSlots = new();
    public List<MixerSlot> sideSlots = new();
    public GameObject mixerPanel;
    public UIPanel inventoryPanel;
    public TMP_Text cookingTimerText;
    public ResultSlot resultSlot;

    public Mixer currentMixer;
    private bool isOpen;
    private PlayerInputActions playerInputActions;

    

    private void Start()
    {
        playerInputActions = Player.Instance.inputActions;

        foreach (var slot in mainSlots)
        {
            slot.OnDropped += HandleDrop;
            slot.OnTakeRequested += HandleTake;
        }

        foreach (var slot in sideSlots)
        {
            slot.OnDropped += HandleDrop;
            slot.OnTakeRequested += HandleTake;
        }
    }

    private void Update()
    {
        if (isOpen)
        {
            if (currentMixer.QueueCount > 0)
            {
                cookingTimerText.text = currentMixer.TimeLeft().ToString();
            }
            else
            {
                cookingTimerText.text = "";
            }

            resultSlot.Show(currentMixer.Ready);
            return;
        }

        if (!playerInputActions.PlayerControl.Interact.WasPressedThisFrame())
            return;

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, 1.5f) 
            && hit.collider.TryGetComponent(out Mixer mixer))
        {
            currentMixer = mixer;
            currentMixer.OnChanged += RefreshSlots;
            Open();
        }
    }

    public void Mix()
    {
        currentMixer.Mix();
    }

    public void CashResult()
    {
        currentMixer.CashResult();
    }

    public void Open()
    {
        if (isOpen)
            return;

        isOpen = true;
        Dragable.DragStarted += HandleDragStarted;
        Dragable.DragEnded += HandleDragEnded;
        inventoryPanel.Open();
        mixerPanel.SetActive(true);
        RefreshSlots();
        IUIPanel.Notify(this, true, false);
    }

    public void Close()
    {
        if (!isOpen)
            return;

        isOpen = false;
        Dragable.DragStarted -= HandleDragStarted;
        Dragable.DragEnded -= HandleDragEnded;
        HandleDragEnded();

        currentMixer.OnChanged -= RefreshSlots;
        currentMixer = null;

        mixerPanel.SetActive(false);
        IUIPanel.Notify(this, false, false);
        inventoryPanel.Close();
    }

    private void HandleDrop(MixerSlot slot, ReagentData reagent)
    {
        int index = mainSlots.IndexOf(slot);
        if (index >= 0)
        {
            currentMixer.TryPlace(true, index, reagent);
            return;
        }

        index = sideSlots.IndexOf(slot);
        currentMixer.TryPlace(false, index, reagent);
    }

    private void HandleTake(MixerSlot slot)
    {
        int index = mainSlots.IndexOf(slot);
        if (index >= 0)
        {
            currentMixer.TryTake(true, index);
            return;
        }

        index = sideSlots.IndexOf(slot);
        currentMixer.TryTake(false, index);
    }

    private void HandleDragStarted(ItemData dragged)
    {
        ReagentData reagent = dragged as ReagentData;
        if (reagent == null)
        {
            HandleDragEnded();
            return;
        }

        for (int i = 0; i < mainSlots.Count; i++)
            mainSlots[i].SetHighlight(currentMixer.AcceptsMain(i, reagent));

        bool sideOn = currentMixer.AcceptsSide(reagent);
        foreach (var slot in sideSlots)
            slot.SetHighlight(sideOn);
    }

    private void HandleDragEnded()
    {
        foreach (var slot in mainSlots)
            slot.SetHighlight(false);

        foreach (var slot in sideSlots)
            slot.SetHighlight(false);
    }

    private void RefreshSlots()
    {
        for (int i = 0; i < mainSlots.Count; i++)
            mainSlots[i].Show(currentMixer.mainItems[i]);

        for (int i = 0; i < sideSlots.Count; i++)
            sideSlots[i].Show(currentMixer.sideItems[i]);
    }
}
