using System;
using UnityEngine;

namespace SfxSystem {
	[RequireComponent(typeof(AudioSource))]
	public class Sfx : MonoBehaviour {
		private AudioSource _source;

		private void Awake() {
			_source = GetComponent<AudioSource>();
		}

		public void Play(AudioClip clip, Vector3 position) {
			_source.Stop();
			transform.position = position;
			_source.clip = clip;
			_source.Play();
		}
	}
}