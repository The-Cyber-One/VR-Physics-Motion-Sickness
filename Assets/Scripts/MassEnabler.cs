using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable), typeof(XRGrabPhysicsInteractable))]
public class MassEnabler : PhysicsEnabler
{
    [SerializeField] private Rigidbody body; // Is allways pressent because XRGrabPhysicsInteractable requires it
    [SerializeField] private float density = 1f;
    [SerializeField] private XRGrabInteractable grabInteractable;
    [SerializeField] private XRGrabPhysicsInteractable grabPhysicsInteractable;

    [ContextMenu("Validate")]
    private void OnValidate()
    {
        if (body == null) body = GetComponent<Rigidbody>();
        if (grabInteractable == null) grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabPhysicsInteractable == null) grabPhysicsInteractable = GetComponent<XRGrabPhysicsInteractable>();

        var size = GetComponent<Collider>().bounds.size;
        var volume = size.x * size.y * size.z;
        body.mass = volume * 1_000 * density;
    }

    private void Awake()
    {
        OnValidate();
    }

    protected override void OnSettingsChanged()
    {
        bool usePhysics = Settings.UseObjectInteraction && body.mass >= Settings.MassMinimumForPhysics;

        grabInteractable.enabled = !usePhysics;
        grabPhysicsInteractable.enabled = usePhysics;
    }
}
