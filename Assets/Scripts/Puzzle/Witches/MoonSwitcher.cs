using System;
using Alchemy;
using UnityEngine;

namespace Witches {
	public class MoonSwitcher : PotionEffectReceiver {
		[SerializeField] private Light _light;
		[SerializeField] private GameObject _shadow;

		private float _initialIntensity;

		protected override PotionType _potionType => PotionType.WitchsBrew;
		
		protected override void OnPotionDrinkAction() {
			_shadow.SetActive(true);
			_light.intensity = 0f;
		}

		protected override void OnPotionEndAction() {
			_shadow.SetActive(false);
			_light.intensity = _initialIntensity;
		}
		
		private void Awake() {
			_initialIntensity = _light.intensity;
		}
	}
}