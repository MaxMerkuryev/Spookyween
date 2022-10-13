using System;
using DG.Tweening;
using Interactable;
using UnityEngine;

namespace Pickupable {
	public class PickupableHolder : PickupableHolderBase, IInteractable {
		[SerializeField] private PickupableType _pickupableType;
		
		private MeshRenderer _hintMeshRenderer;
		private Sequence _hintSequence;
		private static readonly int EmissiveColor = Shader.PropertyToID("_EmissionColor");

		public bool Enabled => true;
		public InteractionType InteractionType => InteractionType.Click;
		public string ActionName {
			get {
				if (CurrentPickupable) return CurrentPickupable.ActionName;
				return PlayerHasRightPickupable() ? $"put {PickupableHolderPlayer.INSTANCE.CurrentPickupable.Name}" : $"need {_pickupableType}";
			}
		}

		public InteractionKeyType KeyType => PlayerHasRightPickupable() || CurrentPickupable ? InteractionKeyType.Default : InteractionKeyType.None;

		public void Interact() {
			Pickupable current = CurrentPickupable;
			bool player = PickupableHolderPlayer.INSTANCE.TryClaimPickupable(_pickupableType, out Pickupable pickupable);
		
			if (current) {
				DropCurrentPickupable();
				PickupableHolderPlayer.INSTANCE.Pickup(current);
			}

			if (player) {
				Pickup(pickupable);
			}
		}

		private bool PlayerHasRightPickupable() {
			return PickupableHolderPlayer.INSTANCE.CurrentPickupable?.Type == _pickupableType;
		}

		private void Awake() {
			_hintMeshRenderer = GetComponent<MeshRenderer>();
		}

		private void OnEnable() {
			PickupableHolderPlayer.INSTANCE.PickupableChanged += UpdateHint;
		}

		private void OnDisable() {
			PickupableHolderPlayer.INSTANCE.PickupableChanged -= UpdateHint;
		}

		private void UpdateHint(PickupableType type) {
			if (_hintMeshRenderer == null) return;
			if (type == _pickupableType) {
				_hintSequence = DOTween.Sequence()
					.Append(_hintMeshRenderer.sharedMaterial
						.DOColor(new Color(0.6f, 0.3f, 0f, 5f), EmissiveColor, 1f))
					.SetLoops(-1, LoopType.Yoyo);
			} else {
				_hintSequence?.Kill();
				_hintMeshRenderer.sharedMaterial.SetColor(EmissiveColor, Color.black);
			}
		}
	}
}