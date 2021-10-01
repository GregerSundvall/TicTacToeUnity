using UnityEngine;

namespace FG {
	[RequireComponent(typeof(AudioSource))]
	public class AudioManager : MonoBehaviour {
		[SerializeField] private AudioClip[] _audioClips;

		private AudioSource _audioSource;

		public void PlayClip(int index) {
			_audioSource.PlayOneShot(_audioClips[index]);
		}

		private void Awake() {
			_audioSource = GetComponent<AudioSource>();
		}
	}
}
