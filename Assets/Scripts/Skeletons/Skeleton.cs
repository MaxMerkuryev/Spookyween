using InteractableSystem;
using PickupableSystem;
using SfxSystem;
using UnityEngine;

namespace Skeletons {
	public class Skeleton : PickupableHolder {
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
			SfxPlayer.Play(SfxType.SkeletonInteract);
			if (CurrentPickupable) {
				if(_puzzle.IsActive) Hypnotize();
				return;
			}
			
			base.Interact();
		}

		public void Hypnotize() {
			_hypnotized = true;
			_handParticles.Stop();
			_handLight.enabled = false;
			_puzzle.SkeletonHypnotized();
			Enabled = false;
			SfxPlayer.Play(SfxType.SkeletonHypnotize);
			(CurrentPickupable as SkeletonPickupable).Hypnotize();
		}
	}
}