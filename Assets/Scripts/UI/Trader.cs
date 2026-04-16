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
        player = Player.Instance;
        _actions = Player.Instance.inputActions;
        _actions.PlayerControl.Interact.performed += ActivateShop;
    }

    
    private void Update()
    {
        playerClose = Vector3.Distance(transform.position, player.transform.position) < 2;

    }
    private void ActivateShop(InputAction.CallbackContext ctx)
    {
        if (playerClose)
            shop.GetComponent<Shop>().Open();
    }
}
