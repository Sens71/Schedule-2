using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIStateManager : MonoBehaviour
{
    public static UIStateManager Instance { get; private set; }

    private readonly Stack<IUIPanel> _stack = new();
    private PlayerInputActions _inputActions;
    private bool _replacing;

    public UIPanel gameMenu;

    public bool IsUIOpen => _stack.Count > 0;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        IUIPanel.OnUIStateChanged += HandlePanelStateChanged;
    }

    void OnDestroy()
    {
        IUIPanel.OnUIStateChanged -= HandlePanelStateChanged;
    }

    void Start()
    {
        _inputActions = Player.Instance.inputActions;
    }

    void Update()
    {
        if (_stack.Count == 0)
        {
            if (_inputActions != null && _inputActions.PlayerControl.Menu.WasPressedThisFrame())
                gameMenu?.Open();
            return;
        }

        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
            _stack.Peek().Close();

        if (_inputActions != null && _inputActions.UI.Switch.WasPressedThisFrame())
            _stack.Peek().Close();
    }

    public void CloseAll()
    {
        while (_stack.Count > 0)
            _stack.Peek().Close();
    }

    public void CloseTop()
    {
        if (_stack.Count > 0)
            _stack.Peek().Close();
    }

    public void CloseUntil(IUIPanel target)
    {
        while (_stack.Count > 0 && _stack.Peek() != target)
            _stack.Peek().Close();
    }

    private void HandlePanelStateChanged(IUIPanel panel, bool isOpen, bool closePrevious)
    {
        if (isOpen)
        {
            if (closePrevious && _stack.Count > 0)
            {
                _replacing = true;
                _stack.Peek().Close();
                _replacing = false;
            }

            _stack.Push(panel);

            if (_stack.Count == 1)
                ApplyInputState(uiMode: true);
        }
        else
        {
            if (_stack.Count > 0 && _stack.Peek() == panel)
            {
                _stack.Pop();

                if (_stack.Count == 0 && !_replacing)
                    ApplyInputState(uiMode: false);
            }
        }
    }

    private void ApplyInputState(bool uiMode)
    {
        if (uiMode)
        {
            _inputActions.PlayerControl.Disable();
            _inputActions.UI.Enable();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            _inputActions.UI.Disable();
            _inputActions.PlayerControl.Enable();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}