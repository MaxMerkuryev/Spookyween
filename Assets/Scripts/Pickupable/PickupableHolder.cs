using Interactable;
using UnityEngine;

namespace Pickupable {
	public class PickupableHolder : PickupableHolderBase, IInteractable {
		[SerializeField] private PickupableType _pickupableType;

		public bool Enabled => true;
		public InteractionType InteractionType => InteractionType.Click;
		public string ActionName {
			get {
				if (CurrentPickupable) return CurrentPickupable.ActionName;
				return PlayerHasRightPickupable() ? $"put {PickupableHolderPlayer.INSTANCE.CurrentPickupable.Name}" : $"need {_pickupableType}";
			}
		}

		public InteractionKeyType KeyType => PlayerHasRightPickupable() || CurrentPickupable ? InteractionKeyType.Default : InteractionKeyType.None;

		public void Interact() {
			Pickupable current = CurrentPickupable;
			bool player = PickupableHolderPlayer.INSTANCE.TryClaimPickupable(out Pickupable pickupable);
		
			if (current) {
				DropCurrentPickupable();
				PickupableHolderPlayer.INSTANCE.Pickup(current);
			}

			if (player) {
				Pickup(pickupable);
			}
		}

		private bool PlayerHasRightPickupable() {
			return PickupableHolderPlayer.INSTANCE.CurrentPickupable?.Type == _pickupableType;
		}
	}
}