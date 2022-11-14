using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
[CanSelectMultiple(false)]
public class XRGrabPhysicsInteractable : XRBaseInteractable
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private float stopSelectingDistance = 1f;

    [ContextMenu("Validate")]
    private void OnValidate()
    {
        if (body == null) body = GetComponent<Rigidbody>();
    }

    protected override void Awake()
    {
        OnValidate();
        base.Awake();
    }

    private void Update()
    {
        if (!isSelected) return;

        Vector3 grabPosition = firstInteractorSelecting.GetAttachPoseOnSelect(this).position;
        Vector3 force = firstInteractorSelecting.transform.position - grabPosition;
        force *= PhysicsManager.Instance.Settings.PlayerStrength;

        if (GetDistanceSqrToInteractor(firstInteractorSelecting) < stopSelectingDistance * stopSelectingDistance)
        {
            Debug.Log($"Force: {force}, with length of {force.magnitude} on object {firstInteractorSelecting}", firstInteractorSelecting.transform);
            body.AddForceAtPosition(force, grabPosition);
        }
    }
}
