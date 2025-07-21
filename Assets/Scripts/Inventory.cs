using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Ghost ghost;
    public float buildingDistance;
    public GameObject buildPanel;
    public GameObject plantPanel;

    void Start()
    {
        
    }

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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {   
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            buildPanel.SetActive(true);
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
            if(Physics.Raycast(screenRay, out hit, buildingDistance))
            {
                ghost.transform.position = hit.point;
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            plantPanel.SetActive(true);
        }
    }
}
