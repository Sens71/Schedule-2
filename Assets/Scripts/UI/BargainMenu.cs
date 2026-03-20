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
    
    void Awake()
    {
        scrollbar.onValueChanged.AddListener(OnValueChange);
    }

    public void SetData(Order order)
    {
        currentOrder = order;
        minPrice.text = order.Price.ToString();
        maxPrice.text = (order.Price* 1.5f).ToString();
        diff = order.Price * 0.5f;
    }
    private void OnValueChange(float value)
    {
        currentPrice.text = (value * diff + currentOrder.Price).ToString();
    }
    

}
