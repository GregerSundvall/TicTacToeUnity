using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

namespace FG {
	[RequireComponent(typeof(Button))]
	public class LoadSceneButton : MonoBehaviour {
		public int sceneIndexToLoad;
		[SerializeField] private FadeCanvasGroup _fadeCanvasGroup;
		public void OnButtonClick() {
			if (_fadeCanvasGroup.IsFading) {
				return;
			}

			StartCoroutine(LoadScene());
		}

		private IEnumerator LoadScene() {
			_fadeCanvasGroup.FadeToOpaque();
			yield return new WaitUntil(() => !this._fadeCanvasGroup.IsFading);
			SceneManager.LoadScene(sceneIndexToLoad);
		}

		private void Awake() {
			GetComponent<Button>()?.onClick.AddListener(OnButtonClick);
		}
	}
}
