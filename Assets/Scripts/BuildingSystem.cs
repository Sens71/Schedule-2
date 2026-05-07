using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public float placementDistance = 10f;

    private Ghost _ghost;
    private PlayerInputActions _inputActions;

    public bool IsPlacing => _ghost != null;

    private void Start()
    {
        _inputActions = Player.Instance.inputActions;
    }

    public void StartPlacing(Ghost ghost)
    {
        if (_ghost != null) Cancel();
        _ghost = Instantiate(ghost);
        UIStateManager.Instance.CloseAll();
    }

    public void Cancel()
    {
        if (_ghost == null) return;
        Destroy(_ghost.gameObject);
        _ghost = null;
    }

    private void Update()
    {
        if (_ghost == null) return;

        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out RaycastHit hit, placementDistance))
            _ghost.transform.position = hit.point;

        if (_inputActions.PlayerControl.Place.WasPressedThisFrame())
        {
            Instantiate(_ghost.prefab, _ghost.transform.position, _ghost.transform.rotation);
            Cancel();
        }
    }
}
