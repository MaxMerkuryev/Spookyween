using System;
using InteractableSystem;
using Ui;
using UnityEngine;

namespace Pickupable {
	public class PickupableHolderPlayer : PickupableHolderBase {
		public static PickupableHolderBase INSTANCE { get; private set; }
		
		private void Awake() => INSTANCE = this;

		private const KeyCode _dropKey = KeyCode.Q;

		private void Update() {
			if(!CurrentPickupable) return;
			InteractionUi.Invoke("Q", $"drop {CurrentPickupable.Name}");
			if (Input.GetKeyDown(_dropKey)) DropCurrentPickupable();
		}

		// oh, that's bad
		public override bool Enabled { get; protected set; } = default;
		public override string ActionName { get; } = default;
		public override InteractionType InteractionType { get; } = default;
		public override InteractionKeyType KeyType { get; } = default;
		public override void Interact() { }
	}
}