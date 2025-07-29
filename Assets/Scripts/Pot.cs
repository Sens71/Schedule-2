using System.Threading.Tasks;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public bool isSelected;
    private MeshRenderer meshRenderer;
    public Transform point;
    private Plant currentPlant = null;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public async Task Select()
    {
        isSelected = true;
       
    }
    private void LateUpdate()
    {
        isSelected = false;
    }
    private void Update()
    {
        if (isSelected && currentPlant == null)
        {   
            meshRenderer.material.color = Color.green;
        }
        else
        {
            meshRenderer.material.color = Color.white;
        }
    }
    public void PlantSeed(Plant plant)
    {   
        if(currentPlant == null)
        {
            currentPlant = Instantiate(plant, point.position, Quaternion.identity);
            meshRenderer.material.color = Color.white;
        }
    }
}
