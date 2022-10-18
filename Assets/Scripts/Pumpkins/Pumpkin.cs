using PickupableSystem;
using UnityEngine;

namespace Pumpkins {
	public class Pumpkin : Pickupable {
		public override string Name => "pumpkin";
		public override PickupableType Type => PickupableType.Pumpkin;
		protected override Vector3 _offset => Vector3.down * 0.25f;
	}
}