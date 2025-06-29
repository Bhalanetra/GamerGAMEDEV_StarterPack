using UnityEngine;
using GamerGAMEDEV.SettingsProperties;
using UnityEngine.UI;

public class SettingsScreen : ScreenBase
{
    #region Variables
    [Space(10)]
    [Header("SETTING OPTIONS")]
    [SerializeField] ToggleAndSliderSettingProperty musicSettingProperty;
    [SerializeField] ToggleAndSliderSettingProperty sfxSettingProperty;

    public ToggleAndSliderActionHolder musicSettingsCallback;
    public ToggleAndSliderActionHolder sfxSettingsCallback;

    #endregion Variables

    #region Unity Methods

    public void Start()
    {
        AddListeners();
    }

    public void OnEnable()
    {
        AddCallbacks();
    }

    public void OnDisable()
    {
        RemoveCallbacks();
    }

    #endregion Unity Methods

    #region Other

    public void DebugToggle(bool toggle)
    {
        Debug.Log($"Toggle Value Changed: {toggle}");
    }

    public void DebugSlider(float slider)
    {
        Debug.Log($"Slider Value Changed: {slider}");
    }

    public override void AddListeners()
    {
        base.AddListeners();
    }

    public override void AddCallbacks()
    {
        musicSettingProperty.onToggleValueChanged += DebugToggle;
        musicSettingProperty.onSliderValueChanged += DebugSlider;

        sfxSettingProperty.onToggleValueChanged += DebugToggle;
        sfxSettingProperty.onSliderValueChanged += DebugSlider;
    }

    public override void RemoveCallbacks()
    {
        musicSettingProperty.onToggleValueChanged -= DebugToggle;
        musicSettingProperty.onSliderValueChanged -= DebugSlider;

        sfxSettingProperty.onToggleValueChanged -= DebugToggle;
        sfxSettingProperty.onSliderValueChanged -= DebugSlider;
    }

    #endregion Other

    #region Main
    #endregion Main
}