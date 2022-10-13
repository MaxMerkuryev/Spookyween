using UnityEngine;

namespace Player {
	public class PlayerController : MonoBehaviour {
		[SerializeField] private float _lookSensitivity = 5f;
		[SerializeField] private Transform _camera;

		[SerializeField] private CapsuleCollider _playerCollider;
		[SerializeField] private LayerMask _groundLayer;
		[SerializeField] private Rigidbody _player;
		[SerializeField] private float _speed = 5f;
		[SerializeField] private float _jumpForce = 15f;
		[SerializeField] private float _gravity = 1f;

		[SerializeField] [Range(0f, 1f)] private float _horizontalDrag = 0.15f;
		[SerializeField] [Range(0f, 1f)] private float _verticalDrag = 0.01f;
		
		private const float _groundCheckSphereOffset = 0.05f;
		private const float _groundCheckRayOffset = 0.5f;
		private const float _slopeFactor = 2f;
		private const float _airSpeedFactor = 3f;

		private Vector3 _movementInput;
		private Vector3 _cameraInput;
		
		private bool _grounded;
		private bool _jumped;

		private float _playerHeight;
		private float _sphereRadius;

		public bool IsGrounded => _grounded;
		public float Speed => _player.velocity.magnitude;
		
		private void Awake() {
			_playerHeight = _playerCollider.height / 2f + _groundCheckRayOffset;
			_sphereRadius = _playerCollider.radius + _groundCheckSphereOffset;
			Application.targetFrameRate = 75;
		}

		private void Start() {
			Cursor.lockState = CursorLockMode.Locked;
		}

		private void Update() {
			HandleCameraInput();
			HandleMovementInput();
			HandleJumpInput();

			RotateCamera();
		}

		private void FixedUpdate() {
			CheckGround();
			MovePlayer();
			ApplyAirDrag();
			ApplyGravity();
			ApplyJump();
		}

		private void RotateCamera() {
			_camera.localEulerAngles = _cameraInput;
		}
		
		private void HandleCameraInput() {
			_cameraInput.x -= Input.GetAxisRaw("Mouse Y") * _lookSensitivity;
			_cameraInput.y += Input.GetAxisRaw("Mouse X") * _lookSensitivity;
			
			_cameraInput.x = Mathf.Clamp(_cameraInput.x, -90f, 90f);
		}
		
		private void HandleMovementInput() {
			Vector3 forward =  Vector3.ProjectOnPlane(_camera.forward, Vector3.up) * Input.GetAxisRaw("Vertical");
			Vector3 right = _camera.right * Input.GetAxisRaw("Horizontal");
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


