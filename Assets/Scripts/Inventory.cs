using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Storage storage;
    public GameObject [] items;

    private TMP_Text text;
    private TMP_Text text1;
    private TMP_Text text2;
    private TMP_Text text3;
    private TMP_Text text4;
    void Start()
    {
        UpdateUI();
        
    }
    private void OnEnable()
    {
        storage.money.OnChange += UpdateUI;
        storage.decSeed.OnChange += UpdateUI;
        storage.canSeed.OnChange += UpdateUI;
        storage.metSeed.OnChange += UpdateUI;
        storage.medPack.OnChange += UpdateUI;
        storage.kSeed.OnChange += UpdateUI;
    }
    private void OnDisable()
    {
        storage.money.OnChange -= UpdateUI;
        storage.decSeed.OnChange -= UpdateUI;
        storage.canSeed.OnChange -= UpdateUI;
        storage.metSeed.OnChange -= UpdateUI;
        storage.medPack.OnChange -= UpdateUI;
        storage.kSeed.OnChange -= UpdateUI;
    }
    private void CreateSlot(ItemData itemData, GameObject slot)
    {
        var textAmount = slot.transform.Find("Amount").GetComponent<TMP_Text>();
        var textName = slot.transform.Find("Name").GetComponent<TMP_Text>();
        var icon = slot.transform.Find("Button").GetComponent<Image>();
        var bg = slot.transform.Find("Bg").GetComponent<Image>();
        textAmount.text = itemData.amount.ToString();
        textName.text = itemData.name.ToString();
        icon.sprite = itemData.icon;
        bg.color = itemData.bgColor;
    }
    private void UpdateUI()
    {
        CreateSlot(storage.money, items[0]);
        CreateSlot(storage.decSeed, items[1]);
        CreateSlot(storage.canSeed, items[2]);
        CreateSlot(storage.kSeed, items[3]);
        CreateSlot(storage.metSeed, items[4]);
        CreateSlot(storage.medPack, items[5]);

    }
    void Update()
    {
        UpdateUI();
    }
}