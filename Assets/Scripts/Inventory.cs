using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Ghost ghost;
    private Plant plant;
    public float buildingDistance;
    public GameObject buildPanel;
    public GameObject plantPanel;
    public GameObject toolPanel;


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
    }
    private void Planting()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (ghost != null)
            {
                Destroy(ghost.gameObject);
                ghost = null;
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            plantPanel.SetActive(true);
            buildPanel.SetActive(false);
            toolPanel.SetActive(false);
        }
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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            plant = null;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            buildPanel.SetActive(true);
            plantPanel.SetActive(false);
            toolPanel.SetActive(false);
        }
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
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (ghost != null)
            {
                Destroy(ghost.gameObject);
                ghost = null;
            }
            plant = null;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            plantPanel.SetActive(false);
            buildPanel.SetActive(false);
            toolPanel.SetActive(true);
        }
    }
}
