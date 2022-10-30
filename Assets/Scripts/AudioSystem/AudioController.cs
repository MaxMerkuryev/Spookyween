using Settings;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioSystem {
	public class AudioController : MonoBehaviour {
		[SerializeField] private Setting _soundsVolumeSetting;
		[SerializeField] private Setting _musicVolumeSetting;

		[SerializeField] private AudioMixer _mixer;
		[SerializeField] private string _soundsVolumeName;
		[SerializeField] private string _musicVolumeName;

		private const float _audioMixerMinValue = -25f;
		private const float _audioMixerMaxValue = 0f;
		private const float _audioMixerThreshold = -80f;
		
		private void Start() {
			UpdateSoundsVolume();
			UpdateMusicVolume();
		}

		private void OnEnable() {
			_soundsVolumeSetting.ValueChanged += UpdateSoundsVolume;
			_musicVolumeSetting.ValueChanged += UpdateMusicVolume;
		}		
		
		private void OnDisable() {
			_soundsVolumeSetting.ValueChanged -= UpdateSoundsVolume;
			_musicVolumeSetting.ValueChanged -= UpdateMusicVolume;
		}

		private void UpdateSoundsVolume() {
			_mixer.SetFloat(_soundsVolumeName, GetVolumeValue(_soundsVolumeSetting.CurrentValue));
		}
		
		private void UpdateMusicVolume() {
			_mixer.SetFloat(_musicVolumeName, GetVolumeValue(_musicVolumeSetting.CurrentValue));
		}

		private float GetVolumeValue(float settingValue) {
			if (settingValue <= 0) return _audioMixerThreshold;
			return Mathf.Lerp(_audioMixerMinValue, _audioMixerMaxValue, settingValue / 100);
		}
	}
}