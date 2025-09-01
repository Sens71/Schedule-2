using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerInputActions inputActions;
    void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Enable();
        inputActions.PlayerControl.Enable();
        inputActions.UI.Disable();
    }
}
