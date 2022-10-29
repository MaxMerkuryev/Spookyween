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
			_mixer.SetFloat(_soundsVolumeName, _soundsVolumeSetting.CurrentValue);
		}
		
		private void UpdateMusicVolume() {
			_mixer.SetFloat(_musicVolumeName, _musicVolumeSetting.CurrentValue);
		}
	}
}