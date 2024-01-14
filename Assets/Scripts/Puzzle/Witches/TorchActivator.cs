using Alchemy;
using UnityEngine;

namespace Witches {
	public class TorchActivator : PotionEffectReceiver {
		[SerializeField] private Torch _torch;
		protected override PotionType _potionType => PotionType.WitchsBrew;
		
		protected override void OnPotionDrinkAction() {
			_torch.Activate();
		}

		protected override void OnPotionEndAction() {
			_torch.Deactivate();
		}
	}
}