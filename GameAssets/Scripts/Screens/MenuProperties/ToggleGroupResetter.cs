using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ToggleGroupResetter : MonoBehaviour
{
    public ToggleGroup toggleGroup;

    void OnEnable()
    {
        ResetToggleGroup();
    }

    public void ResetToggleGroup()
    {
        // Get all toggles in the group
        List<Toggle> toggles = new List<Toggle>(toggleGroup.GetComponentsInChildren<Toggle>());

        // Turn all off first without notifying the group
        foreach (Toggle toggle in toggles)
        {
            toggle.isOn = false;
        }

        // Activate the first toggle
        if (toggles.Count > 0)
        {
            toggles[0].isOn = true;
        }
    }
}
