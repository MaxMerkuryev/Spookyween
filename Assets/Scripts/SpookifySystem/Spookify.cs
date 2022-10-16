using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpookifySystem {
	public class Spookify : MonoBehaviour {
		[SerializeField] private AudioSource _ambienceSource; 
		[SerializeField] private AudioSource _lofiSource;

		[SerializeField] private AudioClip _ambience;
		[SerializeField] private AudioClip[] _lofi;

		private void Awake() {
			_ambienceSource.clip = _ambience;
			_ambienceSource.loop = true;
			_ambienceSource.Play();
			
			PlayLofi();
		}

		private void PlayLofi() {
			_lofiSource.clip = _lofi[Random.Range(0, _lofi.Length)];
			_lofiSource.Play();
			Invoke(nameof(PlayLofi), _lofiSource.clip.length + Random.Range(1f, 5f));
		}
	}
}