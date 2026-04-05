using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public PlayerInputActions inputActions;
    
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        inputActions = new PlayerInputActions();
        inputActions.Enable();
        inputActions.PlayerControl.Enable();
        inputActions.UI.Disable();
    }
}
