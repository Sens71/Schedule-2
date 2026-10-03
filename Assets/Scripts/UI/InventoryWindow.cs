using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class InventoryWindow : MonoBehaviour, IUIPanel
{
    public Storage storage;
    public VisualTreeAsset cardTemplate;

    private VisualElement root;
    private VisualElement window;
    private VisualElement itemsBox;
    private Label emptyLabel;
    private Button closeButton;

    private bool isOpen;

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        root.pickingMode = PickingMode.Ignore;

        window = root.Q<VisualElement>("Window");
        itemsBox = root.Q<VisualElement>("Items");
        emptyLabel = root.Q<Label>("Empty");
        closeButton = root.Q<Button>("CloseButton");

        closeButton.clicked += Close;
        window.style.display = DisplayStyle.None;
    }

    private void OnDisable()
    {
        closeButton.clicked -= Close;
    }

    public void Open()
    {
        if (isOpen)
            return;

        isOpen = true;

        foreach (ItemData item in storage.items)
            item.OnChange += Rebuild;
        storage.OnChange += Rebuild;

        window.style.display = DisplayStyle.Flex;
        Rebuild();
        IUIPanel.Notify(this, true, true);
    }

    public void Close()
    {
        if (!isOpen)
            return;

        isOpen = false;

        foreach (ItemData item in storage.items)
            item.OnChange -= Rebuild;
        storage.OnChange -= Rebuild;

        window.style.display = DisplayStyle.None;
        IUIPanel.Notify(this, false, false);
    }

    private void Rebuild()
    {
        itemsBox.Clear();

        foreach (ItemData item in storage.items)
        {
            if (item.amount <= 0)
                continue;

            AddCard(item.icon, item.name, item.amount, Color.white);
        }

        foreach (Drug drug in storage.Drugs)
        {
            if (drug.amount <= 0)
                continue;

            AddCard(drug.icon, drug.name, drug.amount, drug.iconColor);
        }

        if (itemsBox.childCount == 0)
        {
            emptyLabel.style.display = DisplayStyle.Flex;
        }
        else
        {
            emptyLabel.style.display = DisplayStyle.None;
        }
    }

    private void AddCard(Sprite icon, string title, int amount, Color tint)
    {
        cardTemplate.CloneTree(itemsBox);
        VisualElement card = itemsBox[itemsBox.childCount - 1];

        VisualElement iconElement = card.Q<VisualElement>("Icon");
        iconElement.style.backgroundImage = new StyleBackground(icon);
        iconElement.style.unityBackgroundImageTintColor = tint;

        card.Q<Label>("Title").text = title;
        card.Q<Label>("Amount").text = amount.ToString();
    }
}
