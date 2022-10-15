using System;
using System.Collections.Generic;
using InteractableSystem;
using UnityEngine;

namespace Alchemy {
	public class Potion : Interactable {
		[SerializeField] private MeshRenderer _mesh;
		[SerializeField] private List<PotionConfig> _configs;
		
		private PotionType _potionType;

		public override bool Enabled { get; protected set; } = true;
		public override string ActionName => $"drink {AlchemyData.GetPotionName(_potionType)}";

		public override InteractionType InteractionType => InteractionType.Click;
		public override InteractionKeyType KeyType => InteractionKeyType.Default;
		
		public override void Interact() {
			AlchemyData.OnDrinkPotion?.Invoke(_potionType);
			Destroy(gameObject);
		}

		public void Init(PotionType type) {
			_potionType = type;
			PotionConfig config = _configs.Find(c => c.PotionType == type);
			if(config == null) return;
			_mesh.material = config.Material;
		}
	}

	[Serializable]
	public class PotionConfig {
		public PotionType PotionType;
		public Material Material;
	}
}