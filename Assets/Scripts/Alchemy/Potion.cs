using System;
using System.Collections.Generic;
using DG.Tweening;
using InteractableSystem;
using UnityEngine;

namespace Alchemy {
	public class Potion : Interactable {
		[SerializeField] private MeshRenderer _mesh;
		[SerializeField] private ParticleSystem _particles;
		[SerializeField] private List<PotionConfig> _configs;
		
		private PotionType _potionType;

		public override bool Enabled { get; protected set; } = true;
		public override string ActionName => $"drink {AlchemyData.GetPotionName(_potionType)}";

		public override InteractionType InteractionType => InteractionType.Click;
		public override InteractionKeyType KeyType => InteractionKeyType.Default;
		
		public override void Interact() {
			AlchemyData.OnDrinkPotion?.Invoke(_potionType);
			PotionEffectController.INSTANCE?.Drink(_potionType);
			Destroy(gameObject);
		}

		public void Init(PotionType type) {
			Vector3 initScale = transform.localScale;
			transform.localScale = Vector3.zero;
			transform.DOScale(initScale, 0.3f).SetEase(Ease.OutBack);
			
			_potionType = type;
			PotionConfig config = _configs.Find(c => c.PotionType == type);
			if(config == null) return;
			_mesh.material = config.Material;
			_particles.GetComponent<Renderer>().material = config.Material;
		}
	}

	[Serializable]
	public class PotionConfig {
		public PotionType PotionType;
		public Material Material;
	}
}