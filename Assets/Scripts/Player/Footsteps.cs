using System;
using SfxSystem;
using UnityEngine;

namespace Player {
	public class Footsteps : MonoBehaviour {
		[SerializeField] private PlayerController _playerController;
		[SerializeField] private float _period;
		
		private float _timer;
		
		private void Update() {
			if (_timer > 0) {
				_timer -= Time.deltaTime;
			} else {
				_timer = _period;

				if (!_playerController.IsGrounded) return;
				if (!_playerController.IsWalkin) return;
				SfxPlayer.Play(SfxType.Foot, transform.position);
			}
		}
	}
}