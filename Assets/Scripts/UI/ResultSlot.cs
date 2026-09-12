using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultSlot : MonoBehaviour
{
    public Image icon;
    public TMP_Text amountText;

    public void Show(List<Drug> drugs)
    {
        if (drugs.Count == 0)
        {
            icon.sprite = null;
            icon.color = Color.clear;
            amountText.text = "";
            return;
        }

        icon.sprite = drugs[0].icon;
        icon.color = drugs[0].iconColor;
        amountText.text = drugs.Count.ToString();
    }
}
