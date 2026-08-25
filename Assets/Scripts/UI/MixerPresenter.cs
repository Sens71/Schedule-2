using System.Collections.Generic;
using UnityEngine;

public class MixerPresenter : MonoBehaviour
{
    public List<MixerSlot> mainSlots = new();
    public List<MixerSlot> sideSlots = new();
    public GameObject mixerPanel;
    private MixerModel currentModel;
    private PlayerInputActions playerInputActions;

    private void OpenMixer(MixerModel model)
    {
        currentModel = model;
        mixerPanel.SetActive(true);
        print("mixer found");
    }
    
    void Start()
    {
        playerInputActions = Player.Instance.inputActions;
    }

    
    void Update()
    {
        if (playerInputActions.PlayerControl.Interact.WasPressedThisFrame())
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1.5f))
            {
                if (hit.collider.TryGetComponent<MixerModel>(out MixerModel slot))
                {
                    OpenMixer(slot);
                }
            }
        }
    }
}
