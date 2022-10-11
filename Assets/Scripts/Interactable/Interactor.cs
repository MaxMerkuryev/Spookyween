using Ui;
using UnityEngine;

namespace Interactable {
	public class Interactor : MonoBehaviour {
		[SerializeField] private Transform _cameraHolder;
		private const float _interactionDistance = 3f;
		private const KeyCode _interactionKey = KeyCode.E;
		
		private void Update() {
			Ray ray = new(_cameraHolder.position, _cameraHolder.forward);
			
			if (!Physics.Raycast(ray, out RaycastHit hit, _interactionDistance)) return;
			if (!hit.collider.TryGetComponent(out IInteractable interactable)) return;
			if (!interactable.Enabled) return;
			
			if(GetInteractAction(interactable.InteractionType)) interactable.Interact();
			else InteractionUi.Invoke(GetInteractionKey(interactable.KeyType), interactable.ActionName);
		}

		private string GetInteractionKey(InteractionKeyType keyType) {
			return keyType switch {
				InteractionKeyType.Default => _interactionKey.ToString(),
				InteractionKeyType.None => string.Empty,
				_ => string.Empty
			};
		}
		
		private bool GetInteractAction(InteractionType type) {
			return type switch {
				InteractionType.Click => Input.GetKeyDown(_interactionKey),
				InteractionType.Hold => Input.GetKey(_interactionKey),
				_ => false
			};
		}
	}
}