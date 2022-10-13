using UnityEngine;

namespace InteractableSystem {
	public abstract class Interactable : MonoBehaviour {
		public abstract bool Enabled { get; protected set; }
		public abstract string ActionName { get; }
		public abstract InteractionType InteractionType { get; }
		public abstract InteractionKeyType KeyType { get; }
		public abstract void Interact();

		// duct tape for outlines :)
		private LayerMask _outlineLayer => LayerMask.NameToLayer("Outline");
		private LayerMask _defaultLayer => LayerMask.NameToLayer("Default");
		
		public void Select() {
			if (gameObject.layer == _defaultLayer) {
				gameObject.layer = _outlineLayer;
			}
		}

		public void DeSelect() {
			if (gameObject.layer == _outlineLayer) {
				gameObject.layer = _defaultLayer;
			}
		}
	}

	public enum InteractionType {
		Click,
		Hold
	}

	public enum InteractionKeyType {
		None,
		Default
	}
}