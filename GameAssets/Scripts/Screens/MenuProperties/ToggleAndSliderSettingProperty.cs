using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleAndSliderSettingProperty : MonoBehaviour
{
    #region Variables
    [Header("UI ELEMENTS")]
    [SerializeField] Toggle _toggle;
    [SerializeField] Slider _slider;

    [Space(20)]
    public UnityEvent<bool> _onToggleValueChanged;
    public UnityEvent<float> _onSliderValueChanged;


    public Action<bool> onToggleValueChanged;
    public Action<float> onSliderValueChanged;

    #endregion Variables

    #region Unity Methods

    #endregion Unity Methods

    #region Other

    public void OnToggle(bool value)
    {
        _onToggleValueChanged?.Invoke(value);
        onToggleValueChanged?.Invoke(value);
    }

    public void OnSliderValueChanged(float value)
    {
        _onSliderValueChanged?.Invoke(value);
        onSliderValueChanged?.Invoke(value);
    }

    #endregion Other

    #region Main
    #endregion Main
}