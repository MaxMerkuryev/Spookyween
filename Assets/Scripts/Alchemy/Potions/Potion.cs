using DG.Tweening;
using InteractableSystem;
using Root;
using UnityEngine;

namespace Alchemy.Potions {
	public class Potion : Interactable {
		[SerializeField] private MeshRenderer _mesh;
		[SerializeField] private ParticleSystem _particles;

		public override bool Enabled { get; protected set; } = true;
		public override string ActionName => _config.Name;

		public override InteractionType InteractionType => InteractionType.Click;
		public override InteractionKeyType KeyType => InteractionKeyType.Default;
		
		private PotionConfig _config;

		public void Init(PotionConfig config) {
			_config = config;

			_mesh.material = _config.Material;
			var main = _particles.main;
			main.startColor = _config.Color;

			Vector3 initScale = transform.localScale;
			transform.localScale = Vector3.zero;
			transform.DOScale(initScale, 0.3f).SetEase(Ease.OutBack);
		}

		public override void Interact() {
			EventBus.Instance.Invoke(new PotionDrinkEvent() { Type = _config.Type });
			Destroy(gameObject);
		}
	}

	public struct PotionDrinkEvent : IEvent {
		public PotionType Type;
	}
}
