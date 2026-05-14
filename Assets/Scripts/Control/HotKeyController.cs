using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class HotKeyController : MonoBehaviour   
{
    private PlayerInputActions inputActions;
    [SerializeField] private List<(GameObject, ItemData)> itemMapping = new();
    [SerializeField] private HotKeyDrop[] dropSlots;
    [SerializeField] private GameObject weaponObject;
    [SerializeField] private GameObject weaponObject2;

    
    void Start()
    {
        inputActions = Player.Instance.inputActions;
        inputActions.PlayerControl.Hotkey1.performed += HotKey1;
        inputActions.PlayerControl.Hotkey2.performed += HotKey2;
        inputActions.PlayerControl.Hotkey3.performed += HotKey3;
        inputActions.PlayerControl.Hotkey4.performed += HotKey4;
        inputActions.PlayerControl.Hotkey5.performed += HotKey5;
    }

    private void HotKey1(InputAction.CallbackContext context)
    {
        TryActivateItem(dropSlots[0].item);
    }
    private void HotKey2(InputAction.CallbackContext context)
    {
        TryActivateItem(dropSlots[1].item);
    }
    private void HotKey3(InputAction.CallbackContext context)
    {
        TryActivateItem(dropSlots[2].item);
    }

    private void HotKey4(InputAction.CallbackContext context)
    {
        TryActivateItem(dropSlots[3].item);
    }

    private void HotKey5(InputAction.CallbackContext context)
    {
        TryActivateItem(dropSlots[4].item);
    }

    private void TryActivateItem(ItemData item)
    {   
        if(item == null)
            return;
        if (item.name == "Gun")
        {
            weaponObject.SetActive(true);
            weaponObject2.SetActive(false);
        }

        if (item.name == "Scissors")
        {
            weaponObject.SetActive(false);
            weaponObject2.SetActive(true);
        }
    }
    void Update()
    {
        
    }

}
