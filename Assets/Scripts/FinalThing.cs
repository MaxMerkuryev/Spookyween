using System;
using Player;
using SpookifySystem;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace DefaultNamespace {
	public class FinalThing : MonoBehaviour {
		[SerializeField] private GameObject _player;
		[SerializeField] private GameObject _mainUi;
		[SerializeField] private GameObject _finalUi;
		[SerializeField] private GameObject _spookify;
		[SerializeField] private GameObject _finalCamera;
		[SerializeField] private VolumeProfile _volumeProfile;
		
		private void OnTriggerEnter(Collider other) {
			if (other.TryGetComponent(out PlayerController p)) {
				_player.SetActive(false);
				_finalCamera.SetActive(true);
				_mainUi.SetActive(false);
				_finalUi.SetActive(true);
				_spookify.SetActive(false);
				gameObject.SetActive(false);
				Cursor.lockState = CursorLockMode.None;
				if (_volumeProfile.TryGet(out DepthOfField depthOfField)) {
					depthOfField.active = false;
				}
			}
		}

		private void OnDestroy() {
			if (_volumeProfile.TryGet(out DepthOfField depthOfField)) {
				depthOfField.active = true;
			}
		}
	}
}