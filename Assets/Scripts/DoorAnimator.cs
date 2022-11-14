using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string activatorTag = "Hand";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(activatorTag)) return;

        animator.SetTrigger("Switch");
    }
}
