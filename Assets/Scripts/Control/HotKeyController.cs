using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class HotKeyController : MonoBehaviour   
{
    private PlayerInputActions inputActions;
    [SerializeField] private PlantingSystem plantingSystem;
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
        if(dropSlots[0] == null)
            return;
        TryActivateItem(dropSlots[0].item);
        TryToHeal(dropSlots[0].item);
        plantingSystem.StartPlanting(dropSlots[0].item);
    }
    private void HotKey2(InputAction.CallbackContext context)
    {
        if (dropSlots[1] == null)
            return;
        TryActivateItem(dropSlots[1].item);
        TryToHeal(dropSlots[1].item);
        plantingSystem.StartPlanting(dropSlots[1].item);
    }
    private void HotKey3(InputAction.CallbackContext context)
    {
        if (dropSlots[2] == null)
            return;
        TryActivateItem(dropSlots[2].item);
        TryToHeal(dropSlots[2].item);
        plantingSystem.StartPlanting(dropSlots[2].item);
    }

    private void HotKey4(InputAction.CallbackContext context)
    {
        if (dropSlots[3] == null)
            return;
        TryActivateItem(dropSlots[3].item);
        TryToHeal(dropSlots[3].item);
        plantingSystem.StartPlanting(dropSlots[3].item);
    }

    private void HotKey5(InputAction.CallbackContext context)
    {   
        if (dropSlots[4] == null)
            return;
        TryActivateItem(dropSlots[4].item);
        TryToHeal(dropSlots[4].item);
        plantingSystem.StartPlanting(dropSlots[4].item);
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

    private void TryToHeal(ItemData item)
    {
        if(item.name == "Med" && item.amount >= 1)
        {
            item.amount -= 1;
            Player.Instance.GetComponent<StatsHandler>().ChangeHealth(50);
        }
    }
    void Update()
    {
        
    }

}
