﻿using InteractableSystem;
using UnityEngine;

namespace SpookifySystem {
	public class SpookifySwitch : Interactable {
		[SerializeField] private Spookify _spookify;
		
		public override bool Enabled { get; protected set; } = true;
		public override string ActionName => "next clip";
		public override InteractionType InteractionType => InteractionType.Click;
		public override InteractionKeyType KeyType => InteractionKeyType.Default;

		public override void Interact() {
			if (_spookify.IsOn) _spookify.NextClip();
		}
	}
}