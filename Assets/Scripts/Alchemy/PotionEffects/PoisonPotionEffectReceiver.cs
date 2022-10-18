using Player;
using UnityEngine;

namespace Alchemy.PotionEffects {
	public class PoisonPotionEffectReceiver : PotionEffectReceiver {
		[SerializeField] private PlayerController _playerController;

		protected override PotionType _potionType => PotionType.Poison;
		
		protected override void OnPotionDrinkAction() {
			_playerController.EnablePoisonEffect();
		}

		protected override void OnPotionEndAction() {
			_playerController.DisablePoisonEffect();
		}
	}
}