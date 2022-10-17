using System;
using Alchemy;
using UnityEngine;

namespace Vampires {
	public class VampirePuzzle : PotionEffectReceiver {
		[SerializeField] private VampireCoffin[] _coffins;

		protected override PotionType _potionType => PotionType.VampireBlood;
		
		protected override void OnPotionDrinkAction() {
			foreach (VampireCoffin vampireCoffin in _coffins) {
				vampireCoffin.Open();
			}
		}

		protected override void OnPotionEndAction() {
			foreach (VampireCoffin vampireCoffin in _coffins) {
				vampireCoffin.Close();
			}
		}
	}
}