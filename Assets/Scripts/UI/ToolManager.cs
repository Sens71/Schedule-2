using UnityEngine;
using UnityEngine.InputSystem;

public class ToolManager : MonoBehaviour
{
    private Ghost ghost;
    private Plant plant;
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


    public void BuildObject(Ghost building)
    {
        if (ghost == null)
        {
            ghost = Instantiate(building);
        }
        CloseMenu();
    }

    public void PlantObject(Plant plant)
    {
        if (this.plant == null)
        {
            this.plant = plant;
        }
        CloseMenu();
    }

    public void UseTool(Tool tool)
    {
        tool.gameObject.SetActive(true);
        toolPanel.SetActive(false);
        CloseMenu();
    }

    void Update()
    {
        Building();
        Planting();
        Harvesting();

        if (_inputActions.PlayerControl.Menu.WasPressedThisFrame())
        {
            OpenMenu();
        }
        else if (_inputActions.UI.Switch.WasPressedThisFrame())
        {
            CloseMenu();
        }
    }

    private void OpenMenu()
    {
        controlPanel.SetActive(true);
        toolPanel.SetActive(false);
        buildPanel.SetActive(false);
        plantPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (ghost != null)
        {
            Destroy(ghost.gameObject);
            ghost = null;
        }
        plant = null;

        _inputActions.PlayerControl.Disable();
        _inputActions.UI.Enable();
    }

    private void CloseMenu()
    {
        controlPanel.SetActive(false);
        toolPanel.SetActive(false);
        buildPanel.SetActive(false);
        plantPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _inputActions.UI.Disable();
        _inputActions.PlayerControl.Enable();
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
                        if(plant.seedType.amount > 0)
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
            {
                ghost.transform.position = hit.point;
            }
        }
    }
    private void Harvesting()
    {
        
    }
}
