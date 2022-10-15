using System;
using DG.Tweening;
using InteractableSystem;
using UnityEngine;

namespace PickupableSystem {
	public class PickupableHolder : PickupableHolderBase {
		[SerializeField] private PickupableType _pickupableType;

		public override bool Enabled { get; protected set; } = true;
		public override InteractionType InteractionType => InteractionType.Click;
		public override string ActionName {
			get {
				if (CurrentPickupable) return CurrentPickupable.ActionName;
				return PlayerHasRightPickupable() ? $"put {PickupableHolderPlayer.INSTANCE.CurrentPickupable.Name}" : $"need {_pickupableType}";
			}
		}

		public override InteractionKeyType KeyType => PlayerHasRightPickupable() || CurrentPickupable ? InteractionKeyType.Default : InteractionKeyType.None;

		public override void Interact() {
			Pickupable current = CurrentPickupable;
			bool player = PickupableHolderPlayer.INSTANCE.TryClaimPickupable(_pickupableType, out Pickupable pickupable);
		
			if (current) {
				DropCurrentPickupable();
				PickupableHolderPlayer.INSTANCE.Pickup(current, useCustomOrientation: true);
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