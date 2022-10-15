using DG.Tweening;
using InteractableSystem;
using UnityEngine;

namespace PickupableSystem {
	public abstract class Pickupable : Interactable {
		private Rigidbody _rigidbody;
		private Collider _collider;

		// to avoid wall clipping
		private LayerMask _pickupableLayer => LayerMask.NameToLayer("Pickupable");
		private LayerMask _defaultLayer => LayerMask.NameToLayer("Default");
			
		public abstract string Name { get; }
		public abstract PickupableType Type { get; }

		private Vector3 _initialPosition;
		private Transform _parent;
		
		public override bool Enabled { get; protected set; } = true; 
		public override string ActionName => $"pick up {Name}";
		public override InteractionType InteractionType => InteractionType.Click;
		public override InteractionKeyType KeyType => InteractionKeyType.Default;

		private void Awake() {
			_initialPosition = transform.position;
			_rigidbody = GetComponent<Rigidbody>();
			_collider = GetComponent<Collider>();
		}

		public override void Interact() {
			PickupableHolderPlayer.INSTANCE.Pickup(this);
		}

		public void OnPickup(Transform parent, Vector3[] customPath = null) {
			Enabled = false;
			_rigidbody.velocity *= 0f;
			_rigidbody.angularVelocity *= 0f;
			_rigidbody.isKinematic = true;
			_collider.enabled = false;
			transform.SetParent(parent, true);

			Vector3[] path = customPath ?? new [] {
				Vector3.zero, 
				Vector3.up,
				Vector3.up / 2f 
			};
			
			transform.DOKill();
			transform.DOLocalPath(path, 1f, PathType.CubicBezier).SetEase(Ease.OutBack);
			transform.DOLocalRotate(Vector3.zero, 0.3f);
		}

		public void OnDrop(Vector3 dropOrientation) {
			Enabled = true;
			_collider.enabled = true;
			_rigidbody.isKinematic = false;
			_rigidbody.AddForce(dropOrientation * 5f, ForceMode.Impulse);
			transform.DOKill();
			transform.SetParent(null);
		}

		public void SetPickupableLayer() {
			gameObject.layer = _pickupableLayer;
		}

		public void ResetLayer() {
			gameObject.layer = _defaultLayer;
		}

		public void ResetPickupable() {
			transform.position = _initialPosition;
			OnDrop(Vector3.zero);
		}
	}

	public enum PickupableType {
		None,
		Ingredient,
		Potion,
		Skull,
		Pumpkin
	}
}