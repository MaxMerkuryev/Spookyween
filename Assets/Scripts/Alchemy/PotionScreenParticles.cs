using UnityEngine;

namespace Alchemy {
	[RequireComponent(typeof(ParticleSystem))]
	public class PotionScreenParticles : MonoBehaviour {
		[SerializeField] private PotionConfig _potionConfig;
		
		private ParticleSystem _particleSystem;
		private ParticleSystem.MainModule _particleSystemMainModule;

		private void Awake() {
			_particleSystem = GetComponent<ParticleSystem>();
			_particleSystemMainModule = _particleSystem.main;
		}

		private void OnEnable() {
			PotionEffectController.INSTANCE.OnDrink += OnPotionDrink;
			PotionEffectController.INSTANCE.OnEnd += OnPotionEnd;
		}

		private void OnDisable() {
			PotionEffectController.INSTANCE.OnDrink -= OnPotionDrink;
			PotionEffectController.INSTANCE.OnEnd -= OnPotionEnd;
		}

		private void OnPotionDrink(PotionType type) {
			_particleSystemMainModule.startColor = _potionConfig.GetPotionData(type).ParticleColor;
			_particleSystem.Play();
		}

		private void OnPotionEnd(PotionType type) {
			_particleSystem.Stop();
		}
	}
}