using System;
using Ui;
using UnityEngine;

namespace Player {
	public class PlayerController : Lockable {
		[Header("CAMERA BOB")]
		[SerializeField] private float _amplitude = 0.01f;
		[SerializeField] private float _frequency = 15f;
		[SerializeField] private Transform _camera;

		[Header("LOOK")]
		[SerializeField] private float _lookSensitivity = 5f;
		[SerializeField] private Transform _cameraHolder;

		[Header("BODY")]
		[SerializeField] private CapsuleCollider _playerCollider;
		[SerializeField] private LayerMask _groundLayer;
		[SerializeField] private Rigidbody _player;
		[SerializeField] private float _speed = 5f;
		[SerializeField] private float _jumpForce = 15f;
		[SerializeField] private float _gravity = 1f;

		[SerializeField] [Range(0f, 1f)] private float _horizontalDrag = 0.15f;
		[SerializeField] [Range(0f, 1f)] private float _verticalDrag = 0.01f;
		
		private const float _groundCheckSphereOffset = 0.2f;
		private const float _groundCheckRayOffset = 0.5f;

		private Vector3 _movementInput;
		private Vector3 _cameraInput;
		
		private bool _grounded;
		private bool _jumped;

		private float _playerHeight;
		private float _sphereRadius;
		private float _gravityFactor = 1f;

		private const float _toggleSpeed = 3f;
		private Vector3 CameraMotion => new() { y = Mathf.Sin(Time.time * _frequency) * _amplitude };
		private float HorizontalSpeed => new Vector3(_player.velocity.x,0f, _player.velocity.z).magnitude;

		
		private void Awake() {
			_playerHeight = _playerCollider.height / 2f + _groundCheckRayOffset;
			_sphereRadius = _playerCollider.radius - _groundCheckSphereOffset;
			Application.targetFrameRate = 75;
		}

		private void MoveCamera() {
			_camera.localPosition += CameraMotion;
		}

		private void ResetCamera() {
			_camera.localPosition = Vector3.Lerp(_camera.localPosition, Vector3.zero, 2 * Time.deltaTime);
		}
		
		protected override void OnUpdate() {
			HandleCameraInput();
			HandleMovementInput();
			HandleJumpInput();

			ResetCamera();
			
			if (!_grounded) return;
			if (HorizontalSpeed < _toggleSpeed) return;
			MoveCamera();
		}

		protected override void OnFixedUpdate() {
			CheckGround();
			MovePlayer();
			ApplyAirDrag();
			ApplyGravity();
			ApplyJump();
		}

		protected override void OnLateUpdate() {
			RotateCamera();
		}

		private void RotateCamera() {
			_cameraHolder.localEulerAngles = _cameraInput;
		}
		
		private void HandleCameraInput() {
			_cameraInput.x -= Input.GetAxis("Mouse Y") * _lookSensitivity;
			_cameraInput.y += Input.GetAxis("Mouse X") * _lookSensitivity;
			
			_cameraInput.x = Mathf.Clamp(_cameraInput.x, -90f, 90f);
		}
		
		private void HandleMovementInput() {
			Vector3 forward =  Vector3.ProjectOnPlane(_cameraHolder.forward, Vector3.up) * Input.GetAxisRaw("Vertical");
			Vector3 right = _cameraHolder.right * Input.GetAxisRaw("Horizontal");
			_movementInput = (forward + right).normalized;
		}
		
		private void MovePlayer() {
			_player.velocity += _movementInput * _speed;
		}

		private void ApplyAirDrag() {
			Vector3 velocity = _player.velocity;
			
			Vector3 horizontal = new Vector3(velocity.x, 0f, velocity.z);
			Vector3 vertical = new Vector3(0f, velocity.y, 0f);

			horizontal -= horizontal.normalized * (horizontal.magnitude * _horizontalDrag);
			vertical -= vertical.normalized * (vertical.magnitude * _verticalDrag);
			
			_player.velocity = horizontal + vertical;
		}
		
		private void CheckGround() {
			Vector3 sphereOffset = Vector3.down * (_playerHeight - _sphereRadius);
			_grounded = Physics.CheckSphere(transform.position + sphereOffset, _sphereRadius, _groundLayer);
		}
		
		private void ApplyGravity() {
			if (_grounded) return;
			_player.velocity += Vector3.down * (_gravity * _gravityFactor);
		}

		private void ApplyJump() {
			if (!_jumped) return;
			_jumped = false;
			_player.velocity += Vector3.up * _jumpForce;
		}

		private void HandleJumpInput() {
			if (!_grounded) return;
			if (!Input.GetKeyDown(KeyCode.Space)) return;
			_jumped = true;
		}

		public void EnablePumpkinEffect() {
			_gravityFactor = 0.1f;
			_jumped = true;
		}

		public void DisablePumpkinEffect() {
			_gravityFactor = 1f;
		}
	}
}


