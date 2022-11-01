using UnityEngine;
using UnityEngine.InputSystem;

public class VRHandAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private InputActionProperty pinchAnimationAction;
    [SerializeField] private InputActionProperty gripAnimationAction;

    private void Awake()
    {
        if (animator == null) animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var pinch = pinchAnimationAction.action.ReadValue<float>();
        var grip = gripAnimationAction.action.ReadValue<float>();
        animator.SetFloat("Trigger", pinch);
        animator.SetFloat("Grip", grip);
    }
}
