﻿using InteractableSystem;
using PickupableSystem;
using UnityEngine;
using Vampires;

namespace Skeletons {
	public class Skeleton : PickupableHolder {
		[SerializeField] private SkeletonType _skeletonType;
		[SerializeField] private GameObject _boney;
		[SerializeField] private VampireEyeball[] _eyes;
		[SerializeField] private GameObject _light;

		public override string ActionName => "skeleton";
		public override InteractionType InteractionType => InteractionType.Click;
		public override InteractionKeyType KeyType => InteractionKeyType.Default;

		private bool _hypnotized;
		private SkeletonPuzzle _puzzle;
		public void Init(SkeletonPuzzle puzzle) => _puzzle = puzzle;
		
		public override void Interact() {
			if (_hypnotized) return;
			
			if (CurrentPickupable) {
				if(_puzzle.Active) Hypnotize();
				return;
			}
			
			SkeletonPickupable skeleton = PickupableHolderPlayer.INSTANCE.CurrentPickupable as SkeletonPickupable;

			if (!skeleton) return;
			if (skeleton.SkeletonType != _skeletonType) return;
			
			base.Interact();
		}

		public void Hypnotize() {
			_light.SetActive(true);
			_hypnotized = true;
			_puzzle.SkeletonHypnotized();
			foreach (VampireEyeball eye in _eyes) {
				eye.SetDead();
			}
		}

		public void DeHypnotize() {
			_light.SetActive(false);
			_hypnotized = false;
			foreach (VampireEyeball eye in _eyes) {
				eye.SetDefault();
			}
		}

		public void Die() {
			_light.SetActive(false);
			gameObject.SetActive(false);
			CurrentPickupable?.gameObject.SetActive(false);
			_boney.SetActive(true);
		}
	}

	public enum SkeletonType {
		NoSkull,
		NoHand,
		NoLeg
	}
}