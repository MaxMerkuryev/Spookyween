namespace Interactable {
	public interface IInteractable {
		bool Enabled { get; }
		string ActionName { get; }
		ActionType ActionType { get; }
		void Interact();
	}

	public enum ActionType {
		Click,
		Hold
	}
}