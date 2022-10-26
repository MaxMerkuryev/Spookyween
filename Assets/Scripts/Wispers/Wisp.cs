using System;
using Player;
using SfxSystem;
using UnityEngine;

namespace Wispers {
	public class Wisp : MonoBehaviour {
		[SerializeField] private ParticleSystem _particles;
		[SerializeField] private SphereCollider _collider;
		[SerializeField] private Light _light;
		
		private int _index;
		private Action<int> _onTrigger;

		public void Init(int index, Action<int> action) {
			_index = index;
			_onTrigger = action;
		}
		
		private void OnTriggerEnter(Collider other) {
			if (other.TryGetComponent(out PlayerController p)) {
				_onTrigger?.Invoke(_index);
				SfxPlayer.Play(SfxType.WispTrigger);
			}
		}

		public void Enable() {
			_light.enabled = true;
			_collider.enabled = true;
			_particles.Play();
		}

		public void Disable() {
			_light.enabled = false;
			_collider.enabled = false;
			_particles.Stop();
		}
	}
}