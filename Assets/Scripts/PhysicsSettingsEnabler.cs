using UnityEngine;

public abstract class PhysicsEnabler : MonoBehaviour
{
    protected PhysicsSettings Settings => PhysicsManager.Instance.Settings;

    private void OnEnable()
    {
        Settings.OnSettingsChanged += OnSettingsChanged;
        OnSettingsChanged();
    }

    private void OnDisable()
    {
        Settings.OnSettingsChanged -= OnSettingsChanged;
    }

    /// <summary>
    /// Gets called OnEnable and when a setting has changed
    /// </summary>
    protected abstract void OnSettingsChanged();
}