using System;
using Ui;
using UnityEngine;

namespace Interactable {
	public class Interactor : MonoBehaviour {
		[SerializeField] private Transform _cameraHolder;
		private const float _interactionDistance = 5f;
		private const KeyCode _interactionKey = KeyCode.E;
		
		private void Update() {
			Ray ray = new(_cameraHolder.position, _cameraHolder.forward);
			
			if (!Physics.Raycast(ray, out RaycastHit hit, _interactionDistance)) return;
			if (!hit.collider.TryGetComponent(out IInteractable interactable)) return;
			if (!interactable.Enabled) return;
			
			if(GetInteractAction(interactable.ActionType)) interactable.Interact();
			else InteractionUi.Invoke(_interactionKey.ToString(), interactable.ActionName);
		}

		private bool GetInteractAction(ActionType type) {
			return type switch {
				ActionType.Click => Input.GetKeyDown(_interactionKey),
				ActionType.Hold => Input.GetKey(_interactionKey),
				_ => false
			};
		}
	}
}