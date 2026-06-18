using UnityEngine;
using UnityEngine.UI;

public class MiniMapUIControllert : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private Camera camera;
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;
    void Awake()
    {
        scrollbar.onValueChanged.AddListener(ValueChanged);
    }

    private void ValueChanged(float value)
    {
        float newSize = minValue +(maxValue-minValue) * value;
        camera.orthographicSize = newSize;
        float a = Mathf.Lerp(minValue, maxValue, value);
    }
    
    void Update()
    {
        
    }
}
