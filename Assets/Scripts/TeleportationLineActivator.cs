using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportationLineActivator : MonoBehaviour
{
    [SerializeField] private GameObject leftTeleportation;
    [SerializeField] private InputActionProperty leftAction;

    [SerializeField] private float deadzone = 0.1f;

    private void Update()
    {
        leftTeleportation.SetActive(leftAction.action.ReadValue<Vector2>().y > deadzone);
    }
}
