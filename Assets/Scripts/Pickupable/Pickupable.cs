using DG.Tweening;
using InteractableSystem;
using UnityEngine;

namespace Pickupable {
	public class Pickupable : Interactable {
		[SerializeField] private string _name;
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private Collider _collider;

		public string Name => _name;
		public PickupableType Type;
		
		public override bool Enabled { get; protected set; } = true; 
		public override string ActionName => $"pick up {_name}";
		public override InteractionType InteractionType => InteractionType.Click;
		public override InteractionKeyType KeyType => InteractionKeyType.Default;

		public override void Interact() {
			PickupableHolderPlayer.INSTANCE.Pickup(this);
		}

		public void OnPickup(Transform parent) {
			Enabled = false;
			_rigidbody.velocity *= 0f;
			_rigidbody.angularVelocity *= 0f;
			_rigidbody.isKinematic = true;
			_collider.enabled = false;
			transform.SetParent(parent);

			Vector3[] path = {
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
	}

	public enum PickupableType {
		None,
		Ingredient,
		Potion,
		Skull,
		Pumpkin
	}
}