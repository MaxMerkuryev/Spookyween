using System;
using PickupableSystem;
using UnityEngine;

namespace Pumpkins {
	public class PumpkinHolder : PickupableHolder {
		public override bool Enabled => CurrentPickupable == null;
		public Action OnPickup;

		public override void Pickup(Pickupable pickupable, Vector3[] customPath = null, bool useCustomOrientation = false) {
			base.Pickup(pickupable, customPath, useCustomOrientation);
			OnPickup?.Invoke();
		}
	}
}