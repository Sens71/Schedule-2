using UnityEngine;

public class PlantingSystem : MonoBehaviour
{
    [SerializeField] private SeedRegistry seedRegistry;
    public float plantingDistance = 5f;

    private Plant _plant;
    private PlayerInputActions _inputActions;

    public bool IsPlanting => _plant != null;

    private void Start()
    {
        _inputActions = Player.Instance.inputActions;
    }

    public void StartPlanting(ItemData itemData)
    {
        _plant = seedRegistry.GetPlant(itemData);
        UIStateManager.Instance.CloseAll();
    }

    public void Cancel()
    {
        _plant = null;
    }

    private void Update()
    {
        if (_plant == null) return;

        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out RaycastHit hit, plantingDistance))
        {
            if (hit.collider.TryGetComponent(out Pot pot))
            {
                pot.Select();
                if (_inputActions.PlayerControl.Place.WasPressedThisFrame())
                {
                    if (_plant.seedType.amount > 0)
                    {
                        pot.PlantSeed(_plant);
                        _plant.seedType.ChangeAmount(-1);
                        _plant = null;
                    }
                }
            }
        }
    }
}
