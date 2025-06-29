using System.Collections.Generic;
using UnityEngine;

namespace GamerGAMEDEV.UI
{
    public class ToggleGroup : MonoBehaviour
    {
        [Tooltip("Allow all toggles to be off at once")]
        public bool allowSwitchOff = false;

        private List<Toggle> toggles = new List<Toggle>();

        public void RegisterToggle(Toggle toggle)
        {
            if (!toggles.Contains(toggle))
                toggles.Add(toggle);
        }

        public void UnregisterToggle(Toggle toggle)
        {
            if (toggles.Contains(toggle))
                toggles.Remove(toggle);
        }

        public void NotifyToggleOn(Toggle selected)
        {
            foreach (var t in toggles)
            {
                Debug.Log($"{t.gameObject.name} is same as {selected.gameObject.name} = {t == selected}");
                if (t != selected)
                {
                    t.SetState(false);
                }
            }
        }

        public bool AnyTogglesOn()
        {
            foreach (var t in toggles)
            {
                if (t != null && t.isOn)
                    return true;
            }
            return false;
        }

        public void SetAllOff()
        {
            foreach (var t in toggles)
            {
                t.SetState(false);
            }
        }
    }
}
