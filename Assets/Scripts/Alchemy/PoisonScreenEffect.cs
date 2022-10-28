using System;
using DG.Tweening;
using SfxSystem;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Alchemy {
	public class PoisonScreenEffect : PotionEffectReceiver {
		[SerializeField] private VolumeProfile _volumeProfile;
		private ChromaticAberration _chromaticAberration;

		private void Awake() {
			_volumeProfile.TryGet(out _chromaticAberration);
		}

		protected override PotionType _potionType => PotionType.Poison;
		
		protected override void OnPotionDrinkAction() {
			_chromaticAberration.active = true;
			DOTween.Sequence().InsertCallback(0.3f, ()=> SfxPlayer.Play(SfxType.PoisonEffect));
		}

		protected override void OnPotionEndAction() {
			_chromaticAberration.active = false;
		}

		private void OnDestroy() {
			_chromaticAberration.active = false;
		}
	}
}