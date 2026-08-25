using System.Collections.Generic;
using Unity.VisualScripting;
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
        for (int i = 0; i < mainSlots.Count; i++)
        {
            mainSlots[i].icon.sprite = currentModel.mainItems[i] != null ? currentModel.mainItems[i].icon : null;
            mainSlots[i].icon.color = currentModel.mainItems[i] != null ? Color.white : new Color(0, 0, 0, 0);
        }

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

        if (playerInputActions.PlayerControl.Close.WasPressedThisFrame())
        {
            currentModel = null;
            mixerPanel.SetActive(false);
        }
    }
}
