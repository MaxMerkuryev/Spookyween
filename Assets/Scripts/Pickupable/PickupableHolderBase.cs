using InteractableSystem;
using UnityEngine;

namespace Pickupable {
	public abstract class PickupableHolderBase : Interactable {
		[SerializeField] private Transform _dropOrientaion;
		[SerializeField] private Transform _container;

		public Vector3 DropOrientation => _dropOrientaion.forward;

		public Pickupable CurrentPickupable { get; private set; }

		public void Pickup(Pickupable pickupable) {
			CurrentPickupable?.OnDrop(DropOrientation);
			CurrentPickupable = pickupable;
			CurrentPickupable.OnPickup(_container);
		}

		public bool TryClaimPickupable(PickupableType type, out Pickupable pickupable) {
			if (CurrentPickupable == null || type != CurrentPickupable.Type) {
				pickupable = null;
				return false;
			}
			
			pickupable = CurrentPickupable;
			DropCurrentPickupable();
			return pickupable != null;
		}

		public void DropCurrentPickupable() {
			if (!CurrentPickupable) return;
			CurrentPickupable.OnDrop(DropOrientation);
			CurrentPickupable = null;
		}
	}
}