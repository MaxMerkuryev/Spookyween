using Player;
using UnityEngine;

namespace Alchemy.PotionEffects {
	public class PumpkinPotionEffectReceiver : PotionEffectReceiver {
		[SerializeField] private PlayerController _playerController;

		protected override PotionType _potionType => PotionType.PumpkinJuice;

		protected override void OnPotionDrinkAction() => _playerController.EnablePumpkinEffect();
		protected override void OnPotionEndAction() => _playerController.DisablePumpkinEffect();
	}
}