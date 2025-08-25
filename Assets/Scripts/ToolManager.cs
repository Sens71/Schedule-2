using UnityEngine;

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


    public void BuildObject(Ghost building)
    {   
        if (ghost == null)
        {
            ghost = Instantiate(building);
        }   
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        buildPanel.SetActive(false);
    }
    public void PlantObject(Plant plant)
    {   
        if (this.plant == null)
        {
            this.plant = plant;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        plantPanel.SetActive(false);
        
    }

    public void UseTool(Tool tool)
    {
        tool.gameObject.SetActive(true);
        toolPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    void Update()
    {
        Building();
        Planting();
        Harvesting();
        if (Input.GetKeyDown(KeyCode.Tab))
        {

            if (controlPanel.activeSelf)
            {
                controlPanel.SetActive(false);
                toolPanel.SetActive(false);
                buildPanel.SetActive(false);
                plantPanel.SetActive(false);    
                inventoryPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                controlPanel.SetActive(true);
                toolPanel.SetActive(false);
                buildPanel.SetActive(false);
                plantPanel.SetActive(false);
                inventoryPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                if(ghost != null)
                {
                    Destroy(ghost.gameObject);
                    ghost = null;
                }
                plant = null;
            }
        }
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
                    if (Input.GetKeyDown(KeyCode.F))
                    {

                        pot.PlantSeed(plant);
                        plant = null;
                    }
                }
            }
        }
    }
    private void Building()
    {
        
        if (ghost != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
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
