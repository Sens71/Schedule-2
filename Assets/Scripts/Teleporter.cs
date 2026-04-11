using UnityEngine;
using UnityEngine.InputSystem;

public class Teleporter : MonoBehaviour
{
    public Transform teleportPoint;
    private bool playerPresent;
    private Player _player;
    private PlayerInputActions _inputActions;

    private void Start()
    {
        _inputActions = Player.Instance.inputActions;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            playerPresent = true;
            if (_player == null)
            {
                _player = player;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            playerPresent = false;
        }
    }

    private void Update()
    {
        if (playerPresent && _inputActions.PlayerControl.Interact.WasPressedThisFrame())
        {
            _player.transform.position = teleportPoint.position;
        }
    }
}
