using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GamerGAMEDEV
{
    namespace UI
    {
        public class Toggle : MonoBehaviour, IPointerDownHandler
        {
            public bool interactable = true;

            [Space]
            [SerializeField] Image checkmark;
            [SerializeField] Image Background;
            [SerializeField] TextMeshProUGUI label;
            [Space]
            [Header("TOGGLE")]
            public bool isOn = false;
            [Space]
            [Header("TOGGLE COLOR")]
            [Tooltip("Turn on if Color Changing Required")]
            public bool changeColor = false;
            public Color OnColor = Color.green;
            public Color OffColor = Color.red;
            [Space]
            [Header("TOGGLE TEXT")]
            [Tooltip("Turn onn if Text Switching Required")]
            public bool changeText = false;
            public string onText;
            public string offText;

            [Space]
            [SerializeField] private ToggleGroup toggleGroup;
            public ToggleGroup ToggleGroup
            {
                get => toggleGroup;
                set => toggleGroup = value;
            }

            public void AssignToggleGroup(ToggleGroup toggleGroup)
            {
                this.toggleGroup = toggleGroup;
                this.toggleGroup.RegisterToggle(this);
            }

            [Space]
            public UnityEvent<bool> onValueChanged;

            public void OnClick()
            {
                if(!interactable) return;

                if (isOn)
                {
                    ToggleOff();
                }
                else
                {
                    ToggleOn();
                }

                onValueChanged?.Invoke(isOn);
            }

            public void SetState(bool on)
            {
                if(on) ToggleOn();
                else ToggleOff();
            }

            void ToggleOn()
            {
                isOn = true;
                if (checkmark) checkmark.gameObject.SetActive(true);

                if (changeColor) Background.color = OnColor;
                if (changeText) label.text = onText;

                // Inform the group this toggle is now on
                if (toggleGroup != null && toggleGroup.allowSwitchOff == false)
                {
                    toggleGroup.NotifyToggleOn(this);
                }
            }


            void ToggleOff()
            {
                isOn = false;
                if (checkmark) checkmark.gameObject.SetActive(false);

                if (changeColor) Background.color = OffColor;
                if (changeText) label.text = offText;

                if (!interactable) interactable = true;
            }

            public void OnPointerDown(PointerEventData eventData)
            {
                OnClick();
            }

            void OnEnable()
            {
                if (toggleGroup != null)
                    toggleGroup.RegisterToggle(this);
            }

            void OnDisable()
            {
                if (toggleGroup != null)
                    toggleGroup.UnregisterToggle(this);
            }

        }
    }
}

