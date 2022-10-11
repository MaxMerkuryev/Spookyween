using UnityEngine;

namespace Interactable {
	public class testinteractable : MonoBehaviour, IInteractable {
		public bool Enabled => true;
		public string ActionName => "pick up";
		public ActionType ActionType => ActionType.Hold;
		
		public void Interact() {
			Debug.Log("INTERACTED AAAAAAAA");
		}
	}
}