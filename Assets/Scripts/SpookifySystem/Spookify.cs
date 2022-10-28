using Ui;
using UnityEngine;

namespace SpookifySystem {
	public class Spookify : MonoBehaviour {
		[SerializeField] private AudioSource _ambienceSource; 
		[SerializeField] private AudioSource _lofiSource;

		[SerializeField] private AudioClip _ambience;
		[SerializeField] private AudioClip[] _lofi;

		public bool IsOn { get; private set; }
		private float timer;
			
		private void Awake() {
			_ambienceSource.clip = _ambience;
			_ambienceSource.loop = true;
			_ambienceSource.Play();
		}

		public void Play() {
			_lofiSource.clip = _lofi[Random.Range(0, _lofi.Length)];
			timer = _lofiSource.clip.length + Random.Range(0.1f, 0.5f);
			_lofiSource.Play();
			IsOn = true;
		}

		public void Stop() {
			IsOn = false;
			_lofiSource.Stop();
			timer = 0f;
		}
		
		public void NextClip() {
			Stop();
			Play();
		}

		private void Update() {
			if (IsOn) {
				if (timer > 0) timer -= Time.unscaledDeltaTime;
				else NextClip();
			}
		}
	}
}