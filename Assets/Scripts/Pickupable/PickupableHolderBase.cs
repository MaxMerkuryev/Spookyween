using UnityEngine;

namespace Pickupable {
	public abstract class PickupableHolderBase : MonoBehaviour {
		[SerializeField] private Transform _dropOrientaion;
		[SerializeField] private Transform _container;

		public Vector3 DropOrientation => _dropOrientaion.forward;

		public Pickupable CurrentPickupable { get; private set; }

		public void Pickup(Pickupable pickupable) {
			CurrentPickupable?.OnDrop(DropOrientation);
			CurrentPickupable = pickupable;
			CurrentPickupable.OnPickup(_container);
		}

		public bool TryClaimPickupable(out Pickupable pickupable) {
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