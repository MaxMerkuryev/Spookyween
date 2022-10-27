using PickupableSystem;
using UnityEngine;
using Vampires;

namespace Skeletons {
	public class SkeletonPickupable : Pickupable {
		[SerializeField] private Vector3 _rotationOffset;
		[SerializeField] private Vector3 _positionOffset;
		[SerializeField] private VampireEyeball[] _eyes;
		
		protected override Vector3 _customOrientation => _rotationOffset;
		protected override Vector3 _offset => _positionOffset;
		
		public override string Name => "skull";
		public override PickupableType Type => PickupableType.Skeleton;

		public void Hypnotize() {
			foreach (VampireEyeball eye in _eyes) {
				eye.SetDead();
			}
		}
	}
}