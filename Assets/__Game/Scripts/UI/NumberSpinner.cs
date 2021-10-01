using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace FG {
    public class NumberSpinner : MonoBehaviour {
        public int minimumValue = 0;
        public int maximumValue = 100;
        public IntEvent onValueChanged;

        private TMP_InputField _inputField;

        public int Value { get; set; }

        private void OnValueChanged(string value) {
            if (value.Equals("-")) {
                return;
            }

            int incomingValue = int.Parse(value);

            if (incomingValue < minimumValue || incomingValue > maximumValue) {
                _inputField.SetTextWithoutNotify(Value.ToString());
                return;
            }

            if (Value != incomingValue) {
                Value = incomingValue;
                onValueChanged.Invoke(Value);
                _inputField.SetTextWithoutNotify(value.ToString());
            }
        }

        private void OnButtonUp() {
            OnValueChanged((Value + 1).ToString());
        }

        private void OnButtonDown() {
            OnValueChanged((Value - 1).ToString());
        }
        
        private void SetupSpinnerButtons() {
            Button[] spinnerButtons = GetComponentsInChildren<Button>();
            if (spinnerButtons != null && spinnerButtons.Length == 2) {
                spinnerButtons[0].onClick.AddListener(OnButtonUp);
                spinnerButtons[1].onClick.AddListener(OnButtonDown);
            }
        }

        private void Awake() {
            _inputField = GetComponentInChildren<TMP_InputField>();
            Assert.IsNotNull(_inputField, "_inputField != null");

            _inputField.onValueChanged.AddListener(OnValueChanged);
            OnValueChanged(_inputField.text);

            SetupSpinnerButtons();
        }
    }
}