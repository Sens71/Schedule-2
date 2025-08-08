using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Storage storage;

    public GameObject money;
    public GameObject canSeed;
    public GameObject kSeed;
    public GameObject decSeed;
    public GameObject metSeed;

    private TMP_Text text;
    private TMP_Text text1;
    private TMP_Text text2;
    private TMP_Text text3;
    private TMP_Text text4;
    void Start()
    {
        text = money.transform.Find("Amount").GetComponent<TMP_Text>();
        text1 = canSeed.transform.Find("Amount").GetComponent<TMP_Text>();
        text2 = kSeed.transform.Find("Amount").GetComponent<TMP_Text>();
        text3 = decSeed.transform.Find("Amount").GetComponent<TMP_Text>();
        text4 = metSeed.transform.Find("Amount").GetComponent<TMP_Text>();
    }

    private void UpdateUI()
    {
        
        if (text != null)
        {
            text.text = storage.money.ToString();
            money.SetActive(storage.money > 0);
        }
        
        if (text1 != null)
        {
            text1.text = storage.canSeed.ToString();
            canSeed.SetActive(storage.canSeed > 0);
        }
        
        if (text2 != null)
        {
            text2.text = storage.kSeed.ToString();
            kSeed.SetActive(storage.kSeed > 0);
        }
        
        if (text3 != null)
        {
            text3.text = storage.decSeed.ToString();
            decSeed.SetActive(storage.decSeed > 0);
        }
        
        if (text4 != null)
        {
            text4.text = storage.metSeed.ToString();
            metSeed.SetActive(storage.metSeed > 0);
        }
    }
    void Update()
    {
        UpdateUI();
    }
}
