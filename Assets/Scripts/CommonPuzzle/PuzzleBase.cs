using Alchemy;

namespace CommonPuzzle {
	public abstract class PuzzleBase : PotionEffectReceiver{
		public bool IsActive { get; private set; }

		protected override void OnPotionDrinkAction() {
			IsActive = true;
		}

		protected override void OnPotionEndAction() {
			IsActive = false;
		}
	}
}