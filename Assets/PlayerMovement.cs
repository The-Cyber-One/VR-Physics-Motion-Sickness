using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : PhysicsEnabler
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private InputActionProperty jumpAction;
    [SerializeField] private float jumpTime = 0.5f;
    [SerializeField] private Vector3 jumpVelocity = new(0, 1, 0);

    [ContextMenu("Validate")]
    private void OnValidate()
    {
        if (characterController == null) characterController = GetComponent<CharacterController>();
    }

    private void Awake()
    {
        OnValidate();
    }

    protected override void OnSettingsChanged()
    {
        if (Settings.UseJumping)
        {
            jumpAction.action.performed += Jump;
        }
        else
        {
            jumpAction.action.performed -= Jump;
        }
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        StartCoroutine(C_Jump());
    }

    private bool _jumping = false;
    private IEnumerator C_Jump()
    {
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, groundDistance) && !_jumping)
        {
            _jumping = true;
            var gravity = Physics.gravity;
            Physics.gravity = jumpVelocity;

            yield return new WaitForSeconds(jumpTime);

            Physics.gravity = gravity;
            _jumping = false;
        }
    }
}
