using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BargainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text minPrice;
    [SerializeField] private TMP_Text maxPrice;
    [SerializeField] private TMP_Text currentPrice;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button makeDealButton;
    [SerializeField] private Scrollbar scrollbar;
    private Order currentOrder;
    private float diff;
    private int setPrice;
    private Action _makeDealCallback;
    private Action _cancelBargainCallback;
    
    void Awake()
    {
        scrollbar.onValueChanged.AddListener(OnValueChange);
        closeButton.onClick.AddListener(CancelBargain);
        makeDealButton.onClick.AddListener(MakeAdeal);
    }

    public void SetData(Order order, Action makeDealCallback, Action cancelBargainCallback)
    {
        _makeDealCallback = makeDealCallback;
        _cancelBargainCallback = cancelBargainCallback;
        currentOrder = order;
        minPrice.text = order.Price.ToString();
        maxPrice.text = (order.Price* 1.5f).ToString();
        diff = order.Price * 0.5f;
    }
    private void OnValueChange(float value)
    {
        setPrice = (int)(value * diff + currentOrder.Price);
        currentPrice.text = setPrice.ToString();
    }

    public void MakeAdeal()
    {
        currentOrder.OfferedPrice = setPrice;
        _makeDealCallback?.Invoke();
    }

    public void CancelBargain()
    {
        _cancelBargainCallback?.Invoke();
    }
        
    

}
