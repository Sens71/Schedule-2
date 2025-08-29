using UnityEngine;

public class Plant : MonoBehaviour
{
    public ClockTime growTime;
    private float size;
    private TimeManager timeManager;
    public GameObject particle;
    private bool isSelected;
    public ItemData seedType;
    public void Select()
    {
        isSelected = true;

    }
    private void LateUpdate()
    {
        isSelected = false;
    }

    void Start()
    {
        size = 0.1f;
        transform.localScale = Vector3.one * size;
        timeManager = FindAnyObjectByType<TimeManager>();
    }

    
    void Update()
    {
        if (transform.localScale.x < 1)
        {
            size = size + Time.deltaTime / timeManager.GetRealTime(growTime);
            transform.localScale = Vector3.one * size;
        }

        if (isSelected)
        {
            particle.SetActive(true);
        }
        else
        {
            particle.SetActive(false);
        }


    }
}
