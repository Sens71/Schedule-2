using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform teleportPoint;
    private bool playerPresent;
    private Player _player;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            playerPresent = true;
            if (_player == null)
            {
                _player = player;
            }

        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            playerPresent = false;
        }
    }
    private void Update()
    {
        if (playerPresent && Input.GetKeyDown(KeyCode.E))
        {
            _player.transform.position = teleportPoint.position;
        }
    }
}
