using UnityEngine;

public class LiftActivator : MonoBehaviour
{
    [SerializeField] private Transform xrOrigin;
    [SerializeField] private Transform trackPosition;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float animationLength;
    private bool _track = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!PhysicsManager.Instance.Settings.UsePushableBody && other.CompareTag("Player"))
        {
            playerAnimator.Play("Lift");
            _track = true;
            Invoke(nameof(ResetTrack), animationLength);
        }
    }

    private void ResetTrack() => _track = false;

    private void LateUpdate()
    {
        if (_track)
        {
            xrOrigin.position = trackPosition.position;
        }
    }
}
