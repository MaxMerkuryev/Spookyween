using Ui;
using UnityEngine;

namespace InteractableSystem {
	public class Interactor : MonoBehaviour {
		[SerializeField] private Transform _cameraHolder;
		[SerializeField] private LayerMask _outlineLayer;
		[SerializeField] private LayerMask _defaultLayer;
		
		private const float _interactionDistance = 3f;
		private const KeyCode _interactionKey = KeyCode.E;

		private Interactable _currentInteractable;
		
		private void Update() {
			if(_currentInteractable) _currentInteractable.DeSelect();
			
			Ray ray = new(_cameraHolder.position, _cameraHolder.forward);
			
			if (!Physics.Raycast(ray, out RaycastHit hit, _interactionDistance)) return;
			if (!hit.collider.TryGetComponent(out Interactable interactable)) return;
			if (!interactable.Enabled) return;

			_currentInteractable = interactable;
			_currentInteractable.Select();
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