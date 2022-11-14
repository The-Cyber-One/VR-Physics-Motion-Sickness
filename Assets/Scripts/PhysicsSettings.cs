using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Physics Settings", menuName = "CustomSettings")]
public class PhysicsSettings : ScriptableObject
{
    private bool _usesPhysics;
    public bool UsePhysics;
    public bool UseSmoothLocomotion;
    public bool UseJumping;
    public bool UseObjectInteraction;
    public bool UsePushableBody;
    public float MassMinimumForPhysics = 1f;
    public float PlayerStrength = 10f;

    /// <summary>
    /// Gets called OnEnable and when a setting has changed
    /// </summary>
    public event Action OnSettingsChanged;

    private void OnValidate()
    {
        if (_usesPhysics != UsePhysics)
        {
            foreach (var field in GetType().GetFields())
            {
                if (field.FieldType == typeof(bool))
                {
                    field.SetValue(this, UsePhysics);
                }
            }
        }
        else
        {
            UsePhysics = true;
            foreach (var field in GetType().GetFields())
            {
                if (field.IsPublic && field.FieldType == typeof(bool) && !(bool)field.GetValue(this))
                {
                    UsePhysics = false;
                    break;
                }
            }
        }
        _usesPhysics = UsePhysics;

        OnSettingsChanged?.Invoke();
    }
}
