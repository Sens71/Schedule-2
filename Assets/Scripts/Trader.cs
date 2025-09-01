 using UnityEngine;

public class Trader : MonoBehaviour
{
    private PlayerInputActions _actions;
    public GameObject shopPanel;
    void Start()
    {
        _actions = FindAnyObjectByType<Player>().inputActions;
    }

    private void OpenShop()
    {
        _actions.PlayerControl.Disable();
        _actions.UI.Enable();
        shopPanel.SetActive(true);
    }

}
