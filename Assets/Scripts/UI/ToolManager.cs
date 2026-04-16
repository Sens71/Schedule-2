using UnityEngine;
using UnityEngine.InputSystem;

public class ToolManager : MonoBehaviour, IUIPanel
{
    private Ghost ghost;
    private Plant plant;
    private bool _isOpen;
    public float buildingDistance;
    public GameObject buildPanel;
    public GameObject plantPanel;
    public GameObject toolPanel;
    public GameObject controlPanel;
    public GameObject inventoryPanel;
    public Storage storage;

    private PlayerInputActions _inputActions;

    private void Start()
    {
        _inputActions = Player.Instance.inputActions;
    }

    public void Open()
    {
        if (_isOpen) return;
        _isOpen = true;
        controlPanel.SetActive(true);
        IUIPanel.Notify(this, true, true);
    }

    public void Close()
    {
        if (!_isOpen) return;
        _isOpen = false;
        controlPanel.SetActive(false);
        if (ghost != null) { Destroy(ghost.gameObject); ghost = null; }
        plant = null;
        IUIPanel.Notify(this, false, false);
    }

    public void BuildObject(Ghost building)
    {
        if (ghost == null)
            ghost = Instantiate(building);
        Close();
    }

    public void PlantObject(Plant plant)
    {
        if (this.plant == null)
            this.plant = plant;
        Close();
    }

    public void UseTool(Tool tool)
    {
        tool.gameObject.SetActive(true);
        Close();
    }

    void Update()
    {
        Building();
        Planting();
        Harvesting();

        if (_inputActions.PlayerControl.Menu.WasPressedThisFrame())
            Open();
    }

    private void Planting()
    {
        if (plant != null)
        {
            RaycastHit hit;
            Ray screenRay = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            if (Physics.Raycast(screenRay, out hit, buildingDistance))
            {
                if (hit.collider.TryGetComponent(out Pot pot))
                {
                    pot.Select();
                    if (_inputActions.PlayerControl.Place.WasPressedThisFrame())
                    {
                        if (plant.seedType.amount > 0)
                        {
                            pot.PlantSeed(plant);
                            plant.seedType.ChangeAmount(-1);
                            plant = null;
                        }
                    }
                }
            }
        }
    }

    private void Building()
    {
        if (ghost != null)
        {
            if (_inputActions.PlayerControl.Place.WasPressedThisFrame())
            {
                Instantiate(ghost.prefab, ghost.transform.position, ghost.transform.rotation);
                Destroy(ghost.gameObject);
            }
            RaycastHit hit;
            Ray screenRay = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            if (Physics.Raycast(screenRay, out hit, buildingDistance))
                ghost.transform.position = hit.point;
        }
    }

    private void Harvesting()
    {
    }
}