using Alchemy;
using DG.Tweening;
using PickupableSystem;
using UnityEngine;

namespace Pumpkins {
	// this is trash
	public class PumpkinGreen : Pumpkin {
		private bool _canPickup;
				
		public override void OnPickup(Transform parent, Vector3[] customPath = null, bool useCustomOrientation = false) {
			base.OnPickup(parent, customPath, useCustomOrientation);
			DOTween.Sequence().InsertCallback(0.1f, Reject);
		}
		
		protected override void OnEnable() {
			base.OnEnable();
			PotionEffectController.INSTANCE.OnDrink += OnPotionDrink;
			PotionEffectController.INSTANCE.OnEnd += OnPotionEnd;
		}

		protected override void OnDisable() {
			base.OnDisable();
			PotionEffectController.INSTANCE.OnDrink -= OnPotionDrink;
			PotionEffectController.INSTANCE.OnEnd -= OnPotionEnd;
		}

		private void OnPotionDrink(PotionType type) {
			if (type == PotionType.Poison) _canPickup = true;
		}

		private void OnPotionEnd(PotionType type) {
			_canPickup = false;
			Reject();
		}

		private void Reject() {
			if (!_canPickup && PickupableHolderPlayer.INSTANCE.CurrentPickupable == this) {
				PickupableHolderPlayer.INSTANCE.DropCurrentPickupable();
				ResetLayer();
			}
		}
	}
}