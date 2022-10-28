using InteractableSystem;
using SfxSystem;
using UnityEngine;

namespace SpookifySystem {
	public class SpookifyToggle : Interactable {
		[SerializeField] private Spookify _spookify;
		
		public override bool Enabled { get; protected set; } = true;
		public override string ActionName => _spookify.IsOn ? "toggle off" : "toggle on";
		public override InteractionType InteractionType => InteractionType.Click;
		public override InteractionKeyType KeyType => InteractionKeyType.Default;

		public override void Interact() {
			SfxPlayer.Play(SfxType.SpookifyToggle);
			if (_spookify.IsOn) {
				_spookify.Stop();
			} else {
				_spookify.Play();
			}
		}
	}
}