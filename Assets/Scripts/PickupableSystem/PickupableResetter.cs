using System;
using System.Collections.Generic;
using InteractableSystem;
using UnityEngine;

namespace PickupableSystem {
	public class PickupableResetter : Interactable {
		private static List<Pickupable> _items = new List<Pickupable>();
		
		public static void AddPickupable(Pickupable pickupable) {
			_items.Add(pickupable);	
		}

		public static void RemovePickupable(Pickupable pickupable) {
			_items.Remove(pickupable);	
		}

		public override bool Enabled { get; protected set; } = true;
		public override string ActionName => "sands of time";
		public override InteractionType InteractionType => InteractionType.Click;
		public override InteractionKeyType KeyType => InteractionKeyType.Default;

		private const float _resetTime = 3f;
		private float _currentResetTimer;
		
		public override void Interact() {
			foreach (Pickupable item in _items) {
				item.ResetPickupable();
			}

			_currentResetTimer = _resetTime;
			Enabled = false;
		}

		protected override void OnUpdate() {
			base.OnUpdate();
			if (_currentResetTimer > 0) {
				_currentResetTimer -= Time.deltaTime;
			}
			else {
				if (!Enabled) {
					Enabled = true;
				}
			}
		}
	}
}