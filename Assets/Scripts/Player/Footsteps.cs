using System;
using SfxSystem;
using UnityEngine;

namespace Player {
	public class Footsteps : MonoBehaviour {
		[SerializeField] private PlayerController _playerController;
		[SerializeField] private float _period;
		[SerializeField] private float _periodRun;
		
		private float _timer;
		private float _currentPeriod;
		
		private void Update() {
			_currentPeriod = Input.GetKey(KeyCode.LeftShift) ? _periodRun : _period;	
			
			if (_timer > 0) {
				_timer -= Time.deltaTime;
			} else {
				_timer = _currentPeriod;

				if (!_playerController.IsGrounded) return;
				if (!_playerController.IsWalkin) return;
				SfxPlayer.Play(SfxType.Foot);
			}
		}
	}
}