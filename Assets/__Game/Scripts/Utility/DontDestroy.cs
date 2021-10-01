using UnityEngine;

namespace FG {
	public class DontDestroy : MonoBehaviour {
		private void Awake() {
			DontDestroyOnLoad(gameObject);
		}
	}
}
