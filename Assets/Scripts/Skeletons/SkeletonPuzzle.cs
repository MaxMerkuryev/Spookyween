using System;
using Alchemy;
using CommonPuzzle;
using UnityEngine;

namespace Skeletons {
	public class SkeletonPuzzle : PuzzleBase {
		[SerializeField] private PuzzleTarget _target;
		[SerializeField] private Skeleton[] _skeletons;

		private int _count;
		
		protected override PotionType _potionType => PotionType.Hypnosis;

		public void SkeletonHypnotized() {
			_count++;

			if (_count < _skeletons.Length) return;

			_target.Activate();
		}

		private void Awake() {
			foreach(Skeleton skeleton in _skeletons) skeleton.Init(this);
		}
	}
}