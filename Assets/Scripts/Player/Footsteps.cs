using UnityEngine;

namespace Player {
	public class Footsteps : MonoBehaviour {
		[SerializeField] private PlayerController _playerController;
		[SerializeField] private float _period;
		[SerializeField] private float _periodRun;
		[SerializeField] private AudioSource _source;
		[SerializeField] private AudioClip[] _steps;
		
		private float _timer;
		private float _currentPeriod;
		
		private void Update() {
			_currentPeriod = Input.GetKey(KeyCode.LeftShift) ? _periodRun : _period;	
			
			if (_timer > 0) {
				_timer -= Time.deltaTime;
			} else {
				_timer = _currentPeriod;

				if (!_playerController.IsWalkin) return;
				PlayStep();	
			}
		}

		private int _index;
		private float _pan = 0.15f;
		private float _pitchford = 0.1f;
		
		private void PlayStep() {
			_index = (_index + 1) % _steps.Length;
			_pan = -_pan;

			_source.pitch = 1 + Random.Range(-_pitchford, _pitchford);
			_source.clip = _steps[_index];
			_source.panStereo = _pan;
			
			_source.Play();
		}
	}
}