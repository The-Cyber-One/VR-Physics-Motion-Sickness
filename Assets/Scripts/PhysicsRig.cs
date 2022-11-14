using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRig : MonoBehaviour
{
    public Transform headTransform;
    public Transform leftController, rightController;

    public CapsuleCollider bodyCollider;
    public ConfigurableJoint leftHandJoint, rightHandJoint;

    public float minHeight = 0.5f, maxHeight = 2;

    private void FixedUpdate()
    {
        bodyCollider.height = Mathf.Clamp(headTransform.localPosition.y, minHeight, maxHeight);
        bodyCollider.center = new Vector3(headTransform.localPosition.x, bodyCollider.height / 2, headTransform.localPosition.z);

        leftHandJoint.targetPosition = leftController.localPosition;
        leftHandJoint.targetRotation = leftController.localRotation;

        rightHandJoint.targetPosition = rightController.localPosition;
        rightHandJoint.targetRotation = rightController.localRotation;
    }
}
