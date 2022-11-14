using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PhysicsManager : MonoBehaviour
{
    public static PhysicsManager Instance;
    public PhysicsSettings Settings;

    [SerializeField] private GameObject xrOrigin;

    [Header("Movement")]
    [SerializeField] private ContinuousMoveProviderBase continuousMoveProvider;
    [SerializeField] private ContinuousTurnProviderBase continuousTurnProvider;
    [Space]
    [SerializeField] private TeleportationProvider teleportationProvider;
    [SerializeField] private SnapTurnProviderBase snapTurnProvider;

    [ContextMenu("Validate")]
    private void OnValidate()
    {
        if (xrOrigin == null) xrOrigin = GameObject.Find("XROrigin");

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
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        OnValidate();
    }

    private void OnEnable()
    {
        Settings.OnSettingsChanged += OnSettingsChanged;
        OnSettingsChanged();
    }

    private void OnDisable()
    {
        Settings.OnSettingsChanged -= OnSettingsChanged;
    }

    private void OnSettingsChanged()
    {
        // Smooth locomotion
        continuousMoveProvider.enabled = Settings.UseSmoothLocomotion;
        continuousTurnProvider.enabled = Settings.UseSmoothLocomotion;

        teleportationProvider.enabled = !Settings.UseSmoothLocomotion;
        snapTurnProvider.enabled = !Settings.UseSmoothLocomotion;

    }
}
