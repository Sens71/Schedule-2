using UnityEngine;
using UnityEngine.InputSystem;

public class CapsuleControl : MonoBehaviour
{
    private PlayerInputActions input;
    private Vector3 direction;
    void Start()
    {
        input = new PlayerInputActions();
        input.Enable();
        input.UI.Disable();
        input.UI.Jump.performed += Jump;
        input.UI.Switch.performed += SwitchControl;
        input.PlayerControl.Switch.performed += SwitchControl;
        input.PlayerControl.Move.performed += Move;
        input.PlayerControl.Move.canceled += Stop;

    }
    private void Move(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();
        direction = new Vector3(input.x, 0, input.y);
    }
    private void SwitchControl(InputAction.CallbackContext context)
    {
        if (input.PlayerControl.enabled)
        {
            input.PlayerControl.Disable();
        }
        else
        {
            input.PlayerControl.Enable();
        }
        if (input.UI.enabled)
        {
            input.UI.Disable();
        }
        else
        {
            input.UI.Enable();
        }
    }
    private void Jump(InputAction.CallbackContext context)
    {
        transform.position += Vector3.up;
    }
    private void Stop(InputAction.CallbackContext context)
    {
        direction = Vector3.zero;
    }
    private void Update()
    {
        transform.position += direction * Time.deltaTime;
    }

}
