using TMPro;
using UnityEngine;

namespace FG {
	[DefaultExecutionOrder(-10)]
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class SetPlayerName : MonoBehaviour {
		private TextMeshProUGUI _text;

		public void OnPlayerSwitch(Player player) {
			_text.text = player.displayName;
			_text.color = player.markerColor;
		}
		
		private void Awake() {
			_text = GetComponent<TextMeshProUGUI>();
		}
	}
}
