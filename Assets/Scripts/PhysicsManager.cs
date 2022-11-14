using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PhysicsManager : MonoBehaviour
{
    public static PhysicsManager Instance;
    public PhysicsSettings Settings;

    [SerializeField] private Transform xrOrigin;

    [Header("Movement")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private ContinuousMoveProviderBase continousMoveProvider;
    [SerializeField] private ContinuousTurnProviderBase continuousTurnProvider;
    [Space]
    [SerializeField] private TeleportationProvider teleportationProvider;
    [SerializeField] private SnapTurnProviderBase snapTurnProvider;
    [SerializeField] private TeleportationLineActivator teleportationLineActivator;
    [Space]
    [SerializeField] private GameObject leftHandPhysics;
    [SerializeField] private GameObject rightHandPhysics;

    [ContextMenu("Validate")]
    private void OnValidate()
    {
        if (xrOrigin == null) xrOrigin = GameObject.Find("XR Origin").transform;

        if (xrOrigin != null)
        {
            if (characterController == null) characterController = xrOrigin.GetComponent<CharacterController>();
            if (continousMoveProvider == null) continousMoveProvider = xrOrigin.GetComponent<ContinuousMoveProviderBase>();
            if (continuousTurnProvider == null) continuousTurnProvider = xrOrigin.GetComponent<ContinuousTurnProviderBase>();

            if (teleportationProvider == null) teleportationProvider = xrOrigin.GetComponent<TeleportationProvider>();
            if (snapTurnProvider == null) snapTurnProvider = xrOrigin.GetComponent<SnapTurnProviderBase>();
            if (teleportationLineActivator == null) teleportationLineActivator = xrOrigin.GetComponent<TeleportationLineActivator>();

            if (leftHandPhysics == null) leftHandPhysics = xrOrigin.parent.Find("Left Hand Physics").gameObject;
            if (rightHandPhysics == null) rightHandPhysics = xrOrigin.parent.Find("Right Hand Physics").gameObject;
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
        characterController.enabled = Settings.UseSmoothLocomotion && !Settings.UsePushableBody;
        continousMoveProvider.enabled = Settings.UseSmoothLocomotion && !Settings.UsePushableBody;
        continuousTurnProvider.enabled = Settings.UseSmoothLocomotion;

        teleportationProvider.enabled = !Settings.UseSmoothLocomotion;
        snapTurnProvider.enabled = !Settings.UseSmoothLocomotion;
        teleportationLineActivator.SetActive(!Settings.UseSmoothLocomotion);

        leftHandPhysics.SetActive(Settings.UsePushableBody);
        rightHandPhysics.SetActive(Settings.UsePushableBody);
    }
}
