using System;
using Ui;
using UnityEngine;

namespace Player {
	public class PlayerController : MonoBehaviour {
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
		
		private const float _groundCheckSphereOffset = 0.1f;
		private const float _groundCheckRayOffset = 0.5f;

		private Vector3 _movementInput;
		private Vector3 _cameraInput;
		
		private bool _grounded;
		private bool _jumped;

		private float _playerHeight;
		private float _sphereRadius;

		private bool _locked;
		
		private const float _toggleSpeed = 3f;
		private Vector3 CameraMotion => new() { y = Mathf.Sin(Time.time * _frequency) * _amplitude };
		private float Speed => _player.velocity.magnitude;

		
		private void Awake() {
			_playerHeight = _playerCollider.height / 2f + _groundCheckRayOffset;
			_sphereRadius = _playerCollider.radius - _groundCheckSphereOffset;
			Application.targetFrameRate = 75;
		}

		private void OnEnable() {
			PauseMenu.OnShow += Lock;
			PauseMenu.OnHide += Unlock;
		}

		private void OnDisable() {
			PauseMenu.OnShow -= Lock;
			PauseMenu.OnHide -= Unlock;
		}

		private void Lock() {
			_locked = true;
		}

		private void Unlock() {
			_locked = false;
		}


		private void MoveCamera() {
			_camera.localPosition += CameraMotion;
		}

		private void ResetCamera() {
			_camera.localPosition = Vector3.Lerp(_camera.localPosition, Vector3.zero, Time.deltaTime);
		}
		
		private void Update() {
			if(_locked) return;
			
			HandleCameraInput();
			HandleMovementInput();
			HandleJumpInput();

			RotateCamera();
			ResetCamera();
			
			if (!_grounded) return;
			if (Speed < _toggleSpeed) return;
			MoveCamera();
		}

		private void FixedUpdate() {
			if(_locked) return;
			
			CheckGround();
			MovePlayer();
			ApplyAirDrag();
			ApplyGravity();
			ApplyJump();
		}

		private void RotateCamera() {
			_cameraHolder.localEulerAngles = _cameraInput;
		}
		
		private void HandleCameraInput() {
			_cameraInput.x -= Input.GetAxisRaw("Mouse Y") * _lookSensitivity;
			_cameraInput.y += Input.GetAxisRaw("Mouse X") * _lookSensitivity;
			
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
			_player.velocity += Vector3.down * _gravity;
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
	}
}


