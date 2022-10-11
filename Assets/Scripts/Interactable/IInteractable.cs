namespace Interactable {
	public interface IInteractable {
		bool Enabled { get; }
		string ActionName { get; }
		InteractionType InteractionType { get; }
		InteractionKeyType KeyType { get; }
		void Interact();
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