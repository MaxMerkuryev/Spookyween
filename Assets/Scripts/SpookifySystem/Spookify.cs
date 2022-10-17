using UnityEngine;

namespace SpookifySystem {
	public class Spookify : MonoBehaviour {
		[SerializeField] private AudioSource _ambienceSource; 
		[SerializeField] private AudioSource _lofiSource;

		[SerializeField] private AudioClip _ambience;
		[SerializeField] private AudioClip[] _lofi;

		public bool IsOn { get; private set; }
		
		private void Awake() {
			_ambienceSource.clip = _ambience;
			_ambienceSource.loop = true;
			_ambienceSource.Play();
		}

		public void Play() {
			IsOn = true;
			_lofiSource.clip = _lofi[Random.Range(0, _lofi.Length)];
			_lofiSource.Play();
			Invoke(nameof(Play), _lofiSource.clip.length + Random.Range(1f, 5f));
		}

		public void Stop() {
			IsOn = false;
			_lofiSource.Stop();
		}
		
		public void NextClip() {
			Stop();
			Play();
		}
	}
}