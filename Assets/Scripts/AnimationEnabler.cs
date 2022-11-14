using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]
public class AnimationEnabler : PhysicsEnabler
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody body;

    [ContextMenu("Validate")]
    private void OnValidate()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (body == null) body = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        OnValidate();
    }

    protected override void OnSettingsChanged()
    {
        animator.enabled = !Settings.UseObjectInteraction;
        body.isKinematic = !Settings.UseObjectInteraction;
    }
}
