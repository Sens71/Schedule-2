using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform teleportPoint;

    private void OnTriggerStay(Collider other)
    {
        if (TryGetComponent(out Player player))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.transform.position = teleportPoint.position;
            }
        }
    }
}
