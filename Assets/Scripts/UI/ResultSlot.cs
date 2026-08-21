using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultSlot : MonoBehaviour
{
   [SerializeField] private Image icon;
   [SerializeField] private TMP_Text amountText;
   private List<Drug> currentDrugs = new();
   [SerializeField] private Storage storage;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void AddResult(Drug drug)
    {
        currentDrugs.Add(drug);
        icon.sprite = drug.icon;
        icon.color = drug.iconColor;
        amountText.text = currentDrugs.Count.ToString();
    }

    public void CashResult()
    {
        foreach (var drug in currentDrugs)
        {
            storage.AddDrug(drug);
        }
        currentDrugs.Clear();
        icon.sprite = null;
        icon.color = new Color(0, 0, 0, 0);
        amountText.text = "";
    }
}
