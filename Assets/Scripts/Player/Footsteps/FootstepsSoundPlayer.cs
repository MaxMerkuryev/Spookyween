using UnityEngine;

namespace Spookyween.Player.Footsteps {
	public class FootstepsSoundPlayer : MonoBehaviour {
		[SerializeField] private FootstepsConfig _config;
		[SerializeField] private AudioSource _source;
		
		private IFootstepsProvider _provider;
		private float _timer;
		
		public void Init(IFootstepsProvider provider) {
			_provider = provider;
			_source.panStereo = _config.Pan;
		}

		private void Update() {
			if (!_provider.CanPlay()) return;
			if (Time.time > _timer) return;

			PlayStep();
			UpdateTime();
		}
		
		private void PlayStep() {			
			_source.clip = _config.GetNextClip();
			_source.pitch = _config.GetPitch();
			_source.panStereo = -_source.panStereo;
			
			_source.Play();		
		}

		private void UpdateTime() {
			float period = _provider.IsRunning() ? _config.PeriodRun : _config.Period;
			_timer = Time.time + period;
		}
	}
}