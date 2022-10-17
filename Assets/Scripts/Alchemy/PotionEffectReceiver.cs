using UnityEngine;

namespace Alchemy {
	public abstract class PotionEffectReceiver : MonoBehaviour{
		protected abstract PotionType _potionType { get; }
		protected abstract void OnPotionDrinkAction();
		protected abstract void OnPotionEndAction();
		
		private void OnEnable() {
			PotionEffectController.INSTANCE.OnDrink += OnPotionDrink;
			PotionEffectController.INSTANCE.OnEnd += OnPotionEnd;
		}

		private void OnDisable() {
			PotionEffectController.INSTANCE.OnDrink -= OnPotionDrink;
			PotionEffectController.INSTANCE.OnEnd -= OnPotionEnd;
		}

		private void OnPotionDrink(PotionType type) {
			if(type != _potionType)	return;
			OnPotionDrinkAction();
		}

		private void OnPotionEnd(PotionType type) {
			if(type != _potionType)	return;
			OnPotionEndAction();
		}
	}
}