using PickupableSystem;
using UnityEngine;

namespace Pumpkins {
	public class Pumpkin : Pickupable {
		[SerializeField] private ParticleSystem _particles;
		[SerializeField] private PumpkinType _pumpkinType;
		public PumpkinType PumpkinType => _pumpkinType;
		
		public override string Name => "pumpkin";
		public override PickupableType Type => PickupableType.Pumpkin;
		protected override Vector3 _offset => Vector3.down * 0.25f;
	}

	public enum PumpkinType {
		Red,
		Yellow,
		Blue,
		Purple,
		Dark,
		Green
	}
}