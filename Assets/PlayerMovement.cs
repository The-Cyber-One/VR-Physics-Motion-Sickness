using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : PhysicsEnabler
{
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private CapsuleCollider rigidbodyCollider;

    [Header("Walk")]
    [SerializeField] private InputActionProperty walkAction;
    [SerializeField] private Transform head;
    [SerializeField] private float walkSpeed = 1;

    [Header("Jump")]
    [SerializeField] private InputActionProperty jumpAction;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private float jumpVelocity = 1f;
    [SerializeField] private LayerMask floor;
    [SerializeField] private float gravityJump = 5f;
    [SerializeField] private float gravityJumpTime = 0.1f;

    [ContextMenu("Validate")]
    private void OnValidate()
    {
        if (rigidbody == null) rigidbody = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        OnValidate();
    }

    protected override void OnSettingsChanged()
    {
        bool useWalk = Settings.UsePushableBody && Settings.UseSmoothLocomotion;
        rigidbody.isKinematic = !useWalk;
        rigidbodyCollider.enabled = useWalk;
        if (useWalk) walkAction.action.performed += RigidbodyWalk;
        else walkAction.action.performed -= RigidbodyWalk;

        if (Settings.UseJumping) jumpAction.action.performed += Jump;
        else jumpAction.action.performed -= Jump;
    }

    private void RigidbodyWalk(InputAction.CallbackContext context)
    {
        Vector3 direction = context.ReadValue<Vector2>();
        Vector3 forward = head.forward;
        Vector3 right = head.right;
        forward.y = 0;
        right.y = 0;
        forward = forward.normalized * walkSpeed;
        right = right.normalized * walkSpeed;

        rigidbody.velocity = direction.x * right + direction.y * forward + rigidbody.velocity.y * Vector3.up;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (Settings.UsePushableBody)
        {
            if (Physics.Raycast(transform.position + Vector3.up * groundDistance / 2, Vector3.down, groundDistance, floor))
                rigidbody.AddForce(Vector3.up * jumpVelocity, ForceMode.VelocityChange);
        }
        else
        {
            StartCoroutine(C_Jump());
        }
    }

    private bool _jumping = false;
    private IEnumerator C_Jump()
    {
        if (Physics.Raycast(transform.position + Vector3.up * groundDistance / 2, Vector3.down, groundDistance, floor) && !_jumping)
        {
            _jumping = true;
            var gravity = Physics.gravity;
            Physics.gravity = Vector3.up * gravityJump;

            yield return new WaitForSeconds(gravityJumpTime);

            Physics.gravity = gravity;
            _jumping = false;
        }
    }
}
