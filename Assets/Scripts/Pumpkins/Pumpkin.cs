using PickupableSystem;

namespace Pumpkins {
	public class Pumpkin : Pickupable {
		public override string Name => "pumpkin";
		public override PickupableType Type => PickupableType.Pumpkin;
	}
}