using Alchemy;
using UnityEngine;

namespace Witches {
	public class WitchsPuzzle : PotionEffectReceiver {
		[SerializeField] private Witch[] _witches;

		protected override PotionType _potionType => PotionType.WitchsBrew;
		
		protected override void OnPotionDrinkAction() {
			foreach (Witch witch in _witches) witch.Activate();
		}

		protected override void OnPotionEndAction() {
			foreach (Witch witch in _witches) witch.Deactivate();
		}
	}
}