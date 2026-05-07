using UnityEngine;
using UnityEngine.InputSystem;

public class HotKeyController : MonoBehaviour
{
    private PlayerInputActions inputActions;
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public GameObject object4;
    public GameObject object5;
    
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
        object1.SetActive(true);
        object2.SetActive(false);
        object3.SetActive(false);
        object4.SetActive(false);
        object5.SetActive(false);
    }
    private void HotKey2(InputAction.CallbackContext context)
    {
        object1.SetActive(false);
        object2.SetActive(true);
        object3.SetActive(false);
        object4.SetActive(false);
        object5.SetActive(false);
    }
    private void HotKey3(InputAction.CallbackContext context)
    {
        object1.SetActive(false);
        object2.SetActive(false);
        object3.SetActive(true);
        object4.SetActive(false);
        object5.SetActive(false);
    }

    private void HotKey4(InputAction.CallbackContext context)
    {
        object1.SetActive(false);
        object2.SetActive(false);
        object3.SetActive(false);
        object4.SetActive(true);
        object5.SetActive(false);
    }

    private void HotKey5(InputAction.CallbackContext context)
    {
        object1.SetActive(false);
        object2.SetActive(false);
        object3.SetActive(false);
        object4.SetActive(false);
        object5.SetActive(true);
    }
    void Update()
    {
        
    }
}
