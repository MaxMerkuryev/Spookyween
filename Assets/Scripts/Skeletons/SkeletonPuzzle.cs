using Alchemy;
using CommonPuzzle;
using UnityEngine;

namespace Skeletons {
	public class SkeletonPuzzle : PotionEffectReceiver {
		[SerializeField] private PuzzleTarget _target;
		[SerializeField] private Skeleton[] _skeletons;

		private int _count;
		
		public bool Active { get; private set; }
		
		protected override PotionType _potionType => PotionType.Hypnosis;
		
		protected override void OnPotionDrinkAction() {
			Active = true;
		}

		protected override void OnPotionEndAction() {
			Active = false;
			foreach (Skeleton skeleton in _skeletons) {
				skeleton.DeHypnotize();
			}

			_count = 0;
		}

		public void SkeletonHypnotized() {
			_count++;

			if (_count < _skeletons.Length) return;

			_target.Activate(() => {
				foreach (Skeleton skeleton in _skeletons) {
					skeleton.Die();
				}
			});
		}

		private void Awake() {
			foreach(Skeleton skeleton in _skeletons) skeleton.Init(this);
		}
	}
}