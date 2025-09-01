 using UnityEngine;
using UnityEngine.InputSystem;

public class Trader : MonoBehaviour
{
    private PlayerInputActions _actions;
    private bool playerClose;
    private Player player;
    public GameObject shop;
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        _actions = player.inputActions;
        _actions.PlayerControl.Interact.performed += ActivateShop;
    }

    
    private void Update()
    {
        playerClose = Vector3.Distance(transform.position, player.transform.position) < 2;

    }
    private void ActivateShop(InputAction.CallbackContext ctx)
    {
        if (playerClose)
        {
            shop.SetActive(true);
            _actions.UI.Enable();
            _actions.PlayerControl.Disable();
        }

    }
}
