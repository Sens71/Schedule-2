using UnityEngine;

public class Ghost : MonoBehaviour
{
    private Camera camera;
    void Start()
    {
        camera = Camera.main;
    }

    
    void Update()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if(Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            transform.position = hit.point;
        }
    }
}
