using System;
using System.Collections.Generic;
using UnityEngine;

namespace SfxSystem {
	public class SfxPlayer : MonoBehaviour {
		[SerializeField] private int _poolSize = 20;
		[SerializeField] private SfxConfig _config;
		[SerializeField] private Sfx _sfxPrefab;

		private Queue<Sfx> _sfxPool = new Queue<Sfx>();
		
		private static Action<SfxType, Vector3> _playAction;
		
		public static void Play(SfxType sfxType, Vector3 position) {
			_playAction?.Invoke(sfxType, position);
		}

		private void Awake() {
			for (int i = 0; i < _poolSize; i++) {
				Sfx sfx = Instantiate(_sfxPrefab);
				_sfxPool.Enqueue(sfx);
			}
		}

		private void OnEnable() {
			_playAction += OnPlay;
		}

		private void OnDisable() {
			_playAction -= OnPlay;
		}

		private void OnPlay(SfxType sfxType, Vector3 position) {
			AudioClip clip = _config.GetClip(sfxType);
			if(clip == null) return;
			Sfx sfx = _sfxPool.Dequeue();
			sfx.Play(clip, position);
			_sfxPool.Enqueue(sfx);
		}
	}
}