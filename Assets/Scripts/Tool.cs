using UnityEngine;
using UnityEngine.InputSystem;

public class Tool : MonoBehaviour
{
    public float workDistance;

    private PlayerInputActions _inputActions;

    private void Start()
    {
        _inputActions = Player.Instance.inputActions;
    }

    void Update()
    {
        RaycastHit hit;
        Ray screenRay = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(screenRay, out hit, workDistance))
        {
            var obj = hit.collider.gameObject;
            if(obj.TryGetComponent(out Plant plant))
            {
                if(plant.transform.localScale.x >= 1)
                {
                    plant.Select();
                    if (_inputActions.PlayerControl.Attack.WasPressedThisFrame())
                    {
                        plant.productType.ChangeAmount(1);
                        Destroy(plant.gameObject);
                    }
                }
            }
        }

        if (_inputActions.PlayerControl.SecondaryAction.WasPressedThisFrame())
        {
            gameObject.SetActive(false);
        }
    }
}
