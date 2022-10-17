using PickupableSystem;
using UnityEngine;

namespace Vampires {
	public class Stake : Pickupable {
		public override string Name => "aspen stake";
		public override PickupableType Type => PickupableType.Aspen;
		protected override Vector3 _customOrientation => new Vector3(90f, 0f, 0f);
	}
}