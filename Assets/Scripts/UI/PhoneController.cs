using UnityEngine;

public class PhoneController : MonoBehaviour
{
    [SerializeField] private UIPanel phonePanel;
    [SerializeField] private UIPanel mainMenu;

    public void Open()
    {
        phonePanel.Open();
        mainMenu.Open();
    }

    public void GoHome()
    {
        UIStateManager.Instance.CloseUntil(phonePanel);
        mainMenu.Open();
    }
}
