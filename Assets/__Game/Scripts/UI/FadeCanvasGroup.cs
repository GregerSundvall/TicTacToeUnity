using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace FG {
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeCanvasGroup : MonoBehaviour {
        public float fadeTime = 1f;
        [SerializeField] private bool _fadeToDisabledOnStart = true;
        public FloatEvent fadeDoneEvent;

        private CanvasGroup _canvasGroup;

        public bool IsFading { get; private set; }

        private Coroutine _fadeRoutine;

        public void FadeToOpaque() {
            if (!gameObject.activeSelf) {
                return;
            }
            
            if (IsFading) {
                StopCoroutine(_fadeRoutine);
            }

            _fadeRoutine = StartCoroutine(PerformFade(1f));
        }

        public void FadeToDisabled() {
            if (IsFading) {
                StopCoroutine(_fadeRoutine);
            }

            _fadeRoutine = StartCoroutine(PerformFade(0f));
        }

        private IEnumerator PerformFade(float targetAlpha) {
            IsFading = true;

            float elapsedTime = 0f;
            float currentAlpha = _canvasGroup.alpha;
            float startAlpha = currentAlpha;

            while (elapsedTime < fadeTime) {
                elapsedTime += Time.deltaTime;
                float blend = Mathf.Clamp01(elapsedTime / fadeTime);
                currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, blend);
                _canvasGroup.alpha = currentAlpha;
                yield return null;
            }

            if (targetAlpha >= 0.9f) {
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
            }
            else if (targetAlpha < 0.1f) {
                _canvasGroup.interactable = false;
                _canvasGroup.blocksRaycasts = false;
            }

            fadeDoneEvent.Invoke(targetAlpha);
            IsFading = false;
        }

        private void Start() {
            if (_fadeToDisabledOnStart) {
                FadeToDisabled();
            }
        }

        private void Awake() {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
    }
}
