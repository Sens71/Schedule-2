using UnityEngine;


public class UIPanel : MonoBehaviour, IUIPanel
{
    [Tooltip("Закрыть текущую верхнюю панель стека перед открытием этой")]
    [SerializeField] private bool closePrevious = true;

    private bool _isOpen;

    public void Open()
    {
        if (_isOpen) return;
        _isOpen = true;
        gameObject.SetActive(true);
        IUIPanel.Notify(this, true, closePrevious);
    }

    public void Close()
    {
        if (!_isOpen) return;
        _isOpen = false;
        gameObject.SetActive(false);
        IUIPanel.Notify(this, false, false);
    }
}