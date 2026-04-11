using UnityEngine;
using UnityEngine.InputSystem;

namespace ECM2.Examples.FirstPerson
{
    /// <summary>
    /// First person character input.
    /// </summary>

    public class FirstPersonCharacterInput : MonoBehaviour
    {
        private Character _character;
        private PlayerInputActions _inputActions;

        private void Awake()
        {
            _character = GetComponent<Character>();
        }

        private void Start()
        {
            _inputActions = Player.Instance.inputActions;
        }

        private void Update()
        {
            // Movement input, relative to character's view direction

            Vector2 inputMove = _inputActions.PlayerControl.Move.ReadValue<Vector2>();

            Vector3 movementDirection = Vector3.zero;

            movementDirection += _character.GetRightVector() * inputMove.x;
            movementDirection += _character.GetForwardVector() * inputMove.y;

            _character.SetMovementDirection(movementDirection);

            // Crouch input

            if (_inputActions.PlayerControl.Crouch.WasPressedThisFrame())
                _character.Crouch();
            else if (_inputActions.PlayerControl.Crouch.WasReleasedThisFrame())
                _character.UnCrouch();

            // Jump input

            if (_inputActions.PlayerControl.Jump.WasPressedThisFrame())
                _character.Jump();
            else if (_inputActions.PlayerControl.Jump.WasReleasedThisFrame())
                _character.StopJumping();
        }
    }
}
