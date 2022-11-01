using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Physics Settings", menuName = "CustomSettings")]
public class PhysicsSettings : ScriptableObject
{
    public event Action OnSettingsChanged;
    public bool UseSmoothLocomotion = false;

    private void OnValidate()
    {
        OnSettingsChanged?.Invoke();
    }
}
