using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationLineActivator : MonoBehaviour
{
    [SerializeField] private XRInteractorLineVisual lineVisual;
    [SerializeField] private InputActionProperty startTeleportAction;

    private void Awake()
    {
        if (lineVisual == null) lineVisual = GetComponent<XRInteractorLineVisual>();
    }

    private void Start()
    {
        startTeleportAction.action.started += _ => ShowTeleport(true);
        startTeleportAction.action.canceled += _ => ShowTeleport(false);
        ShowTeleport(false);
    }

    private void ShowTeleport(bool active)
    {
        lineVisual.enabled = active;
        lineVisual.reticle.SetActive(active);
    }
}
