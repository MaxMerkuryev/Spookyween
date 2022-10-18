using System;
using PickupableSystem;
using UnityEngine;

namespace Pumpkins {
	public class PumpkinHolder : PickupableHolder {
		[SerializeField] private PumpkinType _pumpkinType;

		public bool Correct => CurrentPickupable != null && (CurrentPickupable as Pumpkin).PumpkinType == _pumpkinType;
		public override bool Enabled => CurrentPickupable == null;
		public Action OnPickup;

		public override void Pickup(Pickupable pickupable, Vector3[] customPath = null, bool useCustomOrientation = false) {
			base.Pickup(pickupable, customPath, useCustomOrientation);
			OnPickup?.Invoke();
		}
	}
}