using DG.Tweening;
using InteractableSystem;
using Ui;
using UnityEngine;

namespace Pickupable {
	// oh, that's a bad one, but... "it just works"
	public class PickupableHolderPlayer : PickupableHolderBase {
		public static PickupableHolderBase INSTANCE { get; private set; }

		private Sequence _sequence;

		private void Awake() {
			INSTANCE = this;
		}

		private const KeyCode _dropKey = KeyCode.Q;

		private void Update() {
			if(!CurrentPickupable) return;
			InteractionUi.Invoke(_dropKey.ToString(), $"drop {CurrentPickupable.Name}");
			if (Input.GetKeyDown(_dropKey)) DropCurrentPickupable();
		}

		
		public override void Pickup(Pickupable pickupable) {
			base.Pickup(pickupable);
			pickupable.SetPickupableLayer();
			_sequence?.Kill();
			_sequence = DOTween.Sequence();
		}
		
		public override void DropCurrentPickupable() {
			_sequence?.Kill();
			CurrentPickupable?.ResetLayer();
			base.DropCurrentPickupable();
		}
	
		public override bool Enabled { get; protected set; } = default;
		public override string ActionName { get; } = default;
		public override InteractionType InteractionType { get; } = default;
		public override InteractionKeyType KeyType { get; } = default;
		public override void Interact() { }
	}
}