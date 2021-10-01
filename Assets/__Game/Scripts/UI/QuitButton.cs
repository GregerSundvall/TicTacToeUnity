using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace FG {
    [RequireComponent(typeof(Button))]
    public class QuitButton : MonoBehaviour {
        private void OnButtonClick() {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
                Application.Quit();
#endif
        }

        private void Awake() {
            Button button = GetComponent<Button>();
            if (!ReferenceEquals(button, null)) {
                button.onClick.AddListener(OnButtonClick);
            }
        }
    }
}