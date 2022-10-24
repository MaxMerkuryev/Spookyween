using InteractableSystem;
using PickupableSystem;
using UnityEngine;
using Vampires;

namespace Skeletons {
	public class Skeleton : PickupableHolder {
		[SerializeField] private SkeletonType _skeletonType;
		[SerializeField] private VampireEyeball[] _eyes;
		[SerializeField] private ParticleSystem _handParticles;
		[SerializeField] private Light _handLight;

		public override string ActionName => "skeleton";
		public override InteractionType InteractionType => InteractionType.Click;
		public override InteractionKeyType KeyType => InteractionKeyType.Default;

		private bool _hypnotized;
		private SkeletonPuzzle _puzzle;
		public void Init(SkeletonPuzzle puzzle) => _puzzle = puzzle;
		
		public override void Interact() {
			if (_hypnotized) return;
			
			if (CurrentPickupable) {
				if(_puzzle.IsActive) Hypnotize();
				return;
			}
			
			SkeletonPickupable skeleton = PickupableHolderPlayer.INSTANCE.CurrentPickupable as SkeletonPickupable;

			if (!skeleton) return;
			if (skeleton.SkeletonType != _skeletonType) return;
			
			base.Interact();
		}

		public void Hypnotize() {
			_hypnotized = true;
			_handParticles.Stop();
			_handLight.enabled = false;
			_puzzle.SkeletonHypnotized();
			Enabled = false;
			foreach (VampireEyeball eye in _eyes) {
				eye.SetDead();
			}
		}
	}

	public enum SkeletonType {
		NoSkull,
		NoHand,
		NoLeg
	}
}