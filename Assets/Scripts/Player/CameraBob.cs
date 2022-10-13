using UnityEngine;

namespace Player {
	public class CameraBob : MonoBehaviour {
		[SerializeField] private float _amplitude;
		[SerializeField] private float _frequency;

		[SerializeField] private Transform _camera;
		[SerializeField] private PlayerController _playerController;

		private const float _toggleSpeed = 3f;
		private Vector3 CameraMotion => new() { y = Mathf.Sin(Time.time * _frequency) * _amplitude };

		private void MoveCamera() {
			_camera.localPosition += CameraMotion;
		}

		private void ResetCamera() {
			_camera.localPosition = Vector3.Lerp(_camera.localPosition, Vector3.zero, Time.deltaTime);
		}

		private void Update() {
			ResetCamera();
			
			if (!_playerController.IsGrounded) return;
			if (_playerController.Speed < _toggleSpeed) return;
			MoveCamera();
		}
	}
}