using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PhysicsManager : MonoBehaviour
{
    [SerializeField] private PhysicsSettings settings;
    [SerializeField] private GameObject xrOrigin;

    [Header("Movement")]
    [SerializeField] private ContinuousMoveProviderBase continuousMoveProvider;
    [SerializeField] private ContinuousTurnProviderBase continuousTurnProvider;
    [Space]
    [SerializeField] private TeleportationProvider teleportationProvider;
    [SerializeField] private SnapTurnProviderBase snapTurnProvider;

    private void OnValidate() => Validate();

    private void Validate()
    {
        if (xrOrigin == null)
            xrOrigin = GameObject.Find("XROrigin");

        if (xrOrigin != null)
        {
            if (continuousMoveProvider == null) continuousMoveProvider = xrOrigin.GetComponent<ContinuousMoveProviderBase>();
            if (continuousTurnProvider == null) continuousTurnProvider = xrOrigin.GetComponent<ContinuousTurnProviderBase>();
            if (teleportationProvider == null) teleportationProvider = xrOrigin.GetComponent<TeleportationProvider>();
            if (snapTurnProvider == null) snapTurnProvider = xrOrigin.GetComponent<SnapTurnProviderBase>();
        }
    }

    private void Awake()
    {
        Validate();
    }

    private void OnEnable()
    {
        settings.OnSettingsChanged += OnSettingsChanged;
        OnSettingsChanged();
    }

    private void OnDisable()
    {
        settings.OnSettingsChanged -= OnSettingsChanged;
    }

    private void OnSettingsChanged()
    {
        // Smooth locomotion
        continuousMoveProvider.enabled = settings.UseSmoothLocomotion;
        continuousTurnProvider.enabled = settings.UseSmoothLocomotion;

        teleportationProvider.enabled = !settings.UseSmoothLocomotion;
        snapTurnProvider.enabled = !settings.UseSmoothLocomotion;

    }
}
