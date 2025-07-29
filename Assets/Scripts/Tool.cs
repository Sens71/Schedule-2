using UnityEngine;

public class Tool : MonoBehaviour
{
    public float workDistance;

    void Update()
    {
        if (Input.GetMouseButton(0))
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
                        Destroy(plant.gameObject);
                    }
                }
            }
        }
        if (Input.GetMouseButton(1))
        {
            gameObject.SetActive(false);
        }
    }
}
