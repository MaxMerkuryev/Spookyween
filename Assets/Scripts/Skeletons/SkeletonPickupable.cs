using PickupableSystem;
using UnityEngine;

namespace Skeletons {
	public class SkeletonPickupable : Pickupable {
		[SerializeField] private SkeletonType _skeletonType;
		[SerializeField] private Vector3 _rotationOffset;
		[SerializeField] private Vector3 _positionOffset;

		protected override Vector3 _customOrientation => _rotationOffset;
		protected override Vector3 _offset => _positionOffset;

		public SkeletonType SkeletonType => _skeletonType;
		
		public override string Name => _skeletonType switch {
			SkeletonType.NoSkull => "skeleton skull",
			SkeletonType.NoHand => "skeleton arm",
			SkeletonType.NoLeg => "skeleton leg",
			_ => "boomstick" // :)
		};

		public override PickupableType Type => PickupableType.Skeleton;
	}
}