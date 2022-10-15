﻿using DG.Tweening;
using InteractableSystem;
using Ui;
using UnityEngine;

namespace PickupableSystem {
	// it just works
	public class PickupableHolderPlayer : PickupableHolderBase {
		public static PickupableHolderBase INSTANCE { get; private set; }

		private void Awake() {
			INSTANCE = this;
		}

		private const KeyCode _dropKey = KeyCode.Q;
		
		protected override void OnUpdate() {
			if(!CurrentPickupable) return;
			InteractionUi.Invoke(_dropKey.ToString(), $"drop {CurrentPickupable.Name}");
			if (Input.GetKeyDown(_dropKey)) DropCurrentPickupable();
		}
		
		public override void Pickup(Pickupable pickupable, Vector3[] customPath = null) {
			base.Pickup(pickupable, customPath);
			pickupable.SetPickupableLayer();
		}
		
		public override void DropCurrentPickupable() {
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